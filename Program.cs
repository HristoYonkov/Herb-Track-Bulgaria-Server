using Microsoft.EntityFrameworkCore;
using Herb_Track_Bulgaria_Server.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Свързване с Neon
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. CORS политика за Vue проекта
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVue");
app.UseHttpsRedirection();

app.MapGet("/status", () => new { message = "Билбордът на билките работи!", db = "Neon Connected" });

app.Run();