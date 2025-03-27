using System.Linq;
using RTAUI.Models;

namespace RTAUI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Products.Any()) return; // Skip if already seeded

            var products = new Product[]
            {
                new Product { Name = "Smart Room Controller", Description = "AI-powered smart device", Price = 4999 },
                new Product { Name = "RoomTech AI Assistant", Description = "Self-learning AI assistant", Price = 8999 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
