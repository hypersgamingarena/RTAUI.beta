using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using RTAUI.Data;
using RTAUI.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Load Configuration
var configuration = builder.Configuration;

// 🔹 Add MVC Controllers
builder.Services.AddControllersWithViews();

// 🔹 Configure Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// 🔹 Register AI Service
builder.Services.AddScoped<AIInstructionEngine>();

var app = builder.Build();

// 🔹 Auto Database Setup & Migration
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Auto-migrate database
        DbInitializer.Initialize(context); // Auto-seed data
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database Setup Error: {ex.Message}");
    }
}

// 🔹 Middleware Setup
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🔹 Default MVC Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
