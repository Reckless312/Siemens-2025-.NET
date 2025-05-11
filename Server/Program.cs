using Server.Models;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Server.Repository;
using System.Runtime.CompilerServices;
using System;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFilter("Microsoft", LogLevel.Warning);
builder.Logging.AddFilter("System", LogLevel.Warning); 

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

void ShowCommands()
{
    Console.WriteLine("1) get");
    Console.WriteLine("2) add <book_title> <book_author> <book_quality>");
    Console.WriteLine("3) delete-one <book_id>");
    Console.WriteLine("4) delete-all <book_title> <book_author>");
}

Thread commandThread = new Thread(async () =>
{
    const String BASE_URL = "http://localhost:5146";
    const String ADD_COMMAND = "add";
    const String ADD_ROUTE = "/api/books/add";
    const String GET_ROUTE = "/api/books";
    const String GET_COMMAND = "get";
    const String REMOVE_ONE_BOOK_COMMAND = "delete-one";
    const String REMOVE_BOOK_ROUTE = "/api/books/delete/";
    const String REMOVE_BOOKS_COMMAND = "delete-all";


    const int MAIN_COMMAND_INDEX = 0;
    const int TITLE_INDEX = 1;
    const int AUTHOR_INDEX = 2;
    const int QUALITY_INDEX = 3;
    const int ADD_COMMAND_LENGTH = 4;
    const int GET_COMMAND_LENGTH = 1;
    const int REMOVE_ONE_BOOK_COMMAND_LENGTH = 2;
    const int REMOVE_ID_INDEX = 1;
    const int REMOVE_BOOKS_COMMAND_LENGTH = 3;
    const int REMOVE_TITLE_INDEX = 1;
    const int REMOVE_AUTHOR_INDEX = 2;

    HttpClient client = new HttpClient();
    client.BaseAddress = new Uri(BASE_URL);

    Console.WriteLine("Welcome! The following commands are available:");
    Console.WriteLine("Warning! Words inside <?> will be separated by '-'!");
    ShowCommands();

    while (true)
    {
        String? input = Console.ReadLine();

        if (input == null)
        {
            Console.WriteLine("Empty input!");
            ShowCommands();
            continue;
        }

        String[] inputKeywords = input.Split(" ");

        switch (inputKeywords[MAIN_COMMAND_INDEX])
        {
            case ADD_COMMAND:
                if (inputKeywords.Length != ADD_COMMAND_LENGTH)
                {
                    Console.WriteLine("Invalid add command!");
                    ShowCommands();
                    continue;
                }

                String query = $"?title={Uri.EscapeDataString(inputKeywords[TITLE_INDEX].Replace('-', ' '))}" +
                               $"&author={Uri.EscapeDataString(inputKeywords[AUTHOR_INDEX].Replace('-', ' '))}" +
                               $"&quality={Uri.EscapeDataString(inputKeywords[QUALITY_INDEX])}";

                HttpResponseMessage response = await client.PostAsync(ADD_ROUTE + query, null);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Something went wrong!");
                }
                else
                {
                    Console.WriteLine("Book added succesfully!");
                }
                break;
            case GET_COMMAND:
                if (inputKeywords.Length != GET_COMMAND_LENGTH)
                {
                    Console.WriteLine("Invalid get command!");
                    ShowCommands();
                    continue;
                }

                List<Book>? books = await client.GetFromJsonAsync<List<Book>>(GET_ROUTE);

                if (books == null)
                {
                    Console.WriteLine("No books found!");
                    ShowCommands();
                    continue;
                }

                foreach (Book book in books)
                {
                    Console.WriteLine(book.ToString());
                }
                break;
            case REMOVE_ONE_BOOK_COMMAND:
                if (inputKeywords.Length != REMOVE_ONE_BOOK_COMMAND_LENGTH)
                {
                    Console.WriteLine("Invalid remove one book command!");
                    ShowCommands();
                    continue;
                }
                
                HttpResponseMessage deleteOneBookResponse = await client.DeleteAsync(REMOVE_BOOK_ROUTE + inputKeywords[REMOVE_ID_INDEX]);

                if (!deleteOneBookResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book was not removed!");
                    ShowCommands();
                    continue;
                }
                break;
            case REMOVE_BOOKS_COMMAND:
                if (inputKeywords.Length != REMOVE_BOOKS_COMMAND_LENGTH)
                {
                    Console.WriteLine("Invalid remove books command!");
                    ShowCommands();
                    continue;
                }

                HttpResponseMessage deleteBooksResponse = await client.DeleteAsync(REMOVE_BOOK_ROUTE + Uri.EscapeDataString(inputKeywords[REMOVE_TITLE_INDEX].Replace('-', ' ')) + "/" + Uri.EscapeDataString(inputKeywords[REMOVE_AUTHOR_INDEX].Replace('-', ' ')));

                if (!deleteBooksResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Something went wrong!");
                    ShowCommands();
                    continue;
                }
                break;
        }
    }
});

commandThread.IsBackground = true;
commandThread.Start();

app.Run();
