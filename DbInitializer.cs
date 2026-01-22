using Herb_Track_Bulgaria_Server.Data;
using Herb_Track_Bulgaria_Server.Models;

namespace Herb_Track_Bulgaria_Server.Data
{
    public static class DbInitializer
    {
        public static void Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Проверка и добавяне на данни
                if (!context.Herbs.Any())
                {
                    context.Herbs.AddRange(
                        new Herb { Name = "Мащерка", LatinName = "Thymus", Description = "...", Price = 5.50m },
                        new Herb { Name = "Лавандула", LatinName = "Lavandula", Description = "...", Price = 8.20m }
                    );
                    context.SaveChanges();
                    Console.WriteLine("--- Базата данни беше сийдната успешно! ---");
                }
            }
        }
    }
}