var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Operator>();
builder.Services.AddSingleton<ILogger>((sp) => 
{
    var consoleLogger = ConsoleLogger.Instance;
    consoleLogger.NeedWriteFullDate = false;
    var logger = new MultiLogger(new FileLogger(Assembly.GetEntryAssembly().GetName().Name), consoleLogger);
    logger.LogLevel = LogLevel.Trace;
    return logger;
});
builder.Services.AddSingleton<IReadWriteHistoryOfTransactions, ReadWriteHistoryOfTransactionsFromFile>();
builder.Services.AddSingleton<IUserKeeper, UserFileKeeper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
