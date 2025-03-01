using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ExerciseTracker.Coordinators;
using Microsoft.Extensions.DependencyInjection;
using ExerciseTracker.Data;
using Microsoft.EntityFrameworkCore;
using ExerciseTracker.Repository;
using ExerciseTracker.Utilities;
using ExerciseTracker.Interface;
using ExerciseTracker.Services;
using ExerciseTracker.Controllers;
using ExerciseTracker.Display;
using ExerciseTracker.Interfaces;
using ExerciseTracker.Models;
using System.Threading.Tasks;

namespace ExerciseTracker;

class Program
{
    static async Task Main(string[] args)
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");

        // Loads settings from JSON file and builds configuration object
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();

        // Register services
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddSingleton<UserInput>();
        builder.Services.AddSingleton<Validation>();
        builder.Services.AddSingleton<DateTimeParser>();
        builder.Services.AddScoped<ExerciseController>();
        builder.Services.AddScoped<ExerciseService>();
        builder.Services.AddSingleton<DisplayManager>();
        builder.Services.AddSingleton<IExerciseMapper, ExerciseMapper>();
        builder.Services.AddScoped(typeof(IExerciseRepository<>), typeof(ExerciseRepository<>));
        builder.Services.AddScoped<ApplicationCoordinator>();

        // Build app from services
        var app = builder.Build();

        // Create a scope for DI management
        using var scope = app.Services.CreateScope();
        var appCoordinator = scope.ServiceProvider.GetRequiredService<ApplicationCoordinator>();

        await appCoordinator.Start();
    }
}
