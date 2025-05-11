WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
