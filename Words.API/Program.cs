using Words.API.Hubs;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                          policy.WithOrigins("http://localhost:5173");
                          policy.AllowCredentials();
                      });
});

builder.Services.AddSignalR(opt =>
{
    opt.EnableDetailedErrors = true;
});

var app = builder.Build();

app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
                          .WithOrigins("http://localhost:5173")
               .AllowCredentials()); // allow credentials

app.MapHub<WordsHub>("/wordshub");
app.MapGet("/", () => "Hello World!");

app.Run();
