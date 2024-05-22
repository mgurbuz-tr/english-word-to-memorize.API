using Authentication.API.AuthInitialData;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
var carterOptionsBuilder = new Carter.CarterConfigurator()
builder.Services.AddCarter(null,opt =>
{
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<UserInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//TODO : Add Healthchecks!

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(opt => { });

app.Run();
