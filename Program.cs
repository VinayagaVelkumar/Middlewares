using Middlewares.Middleware.Logging;
using Middlewares.Middleware.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Logs the incoming request and response status and time elapsed
app.UseRequestLogging();

// Limits the incoming request from same IP 1 per second
app.UseRateLimiting();

app.MapControllers();

app.Run();
