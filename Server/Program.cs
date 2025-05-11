using Server.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Server.Repository;
using System.Runtime.CompilerServices;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

String connectionStringLabel = "DefaultConnection";

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(connectionStringLabel)));
builder.Services.AddScoped<BookRepository, BookRepository>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Thread commandThread = new Thread(async () =>
{
    const String BASE_URL = "http://localhost:5146";

    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri(BASE_URL);

    const int MAIN_COMMAND_INDEX = 0;

    const String ADD_COMMAND = "add";
    const int TITLE_INDEX = 1;
    const int AUTHOR_INDEX = 2;
    const int QUALITY_INDEX = 3;
    const int ADD_COMMAND_LENGTH = 4;

    const String ADD_ROUTE = "/api/books/add";
    while (true)
    {
        String? input = Console.ReadLine();

        if (input == null)
        {
            continue;
        }

        String[] inputKeywords = input.Split(" ");

        switch (inputKeywords[MAIN_COMMAND_INDEX]) 
        {
            case ADD_COMMAND:
                if (inputKeywords.Length != ADD_COMMAND_LENGTH)
                {
                    continue;
                }

                String query = $"?title={Uri.EscapeDataString(inputKeywords[TITLE_INDEX].Replace('-', ' '))}" +
                               $"&author={Uri.EscapeDataString(inputKeywords[AUTHOR_INDEX].Replace('-', ' '))}" +
                               $"&quality={Uri.EscapeDataString(inputKeywords[QUALITY_INDEX])}";

                HttpResponseMessage response = await client.PostAsync(ADD_ROUTE + query, null);
                break;
        }
    }
});

commandThread.IsBackground = true;
commandThread.Start();

app.Run();
