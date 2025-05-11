using Server.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Server.Repository;

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

Thread commandThread = new Thread(() =>
{
    while (true)
    {
        String? input = Console.ReadLine();
        Console.WriteLine(input);
    }
});

commandThread.IsBackground = true;
commandThread.Start();

app.Run();
