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
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowVue");
app.UseHttpsRedirection();

app.MapGet("/status", () => new { message = "Билбордът на билките работи!", db = "Neon Connected" });
app.MapControllers();

// Seed Data Logic
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // Проверява дали вече има билки, за да не ги дублира при всеки рестарт
    if (!context.Herbs.Any())
    {
        context.Herbs.AddRange(
            new Herb_Track_Bulgaria_Server.Models.Herb
            {
                Name = "Мащерка",
                LatinName = "Thymus",
                Description = "Многогодишно тревисто растение с приятен аромат.",
                Price = 5.50m
            },
            new Herb_Track_Bulgaria_Server.Models.Herb
            {
                Name = "Лавандула",
                LatinName = "Lavandula",
                Description = "Използва се за етерични масла и успокоение.",
                Price = 8.20m
            }
        );
        context.SaveChanges();
        Console.WriteLine("--- Данните бяха сийднати успешно! ---");
    }
}

app.Run();