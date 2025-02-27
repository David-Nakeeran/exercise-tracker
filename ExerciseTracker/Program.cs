using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ExerciseTracker.Coordinators;
using Microsoft.Extensions.DependencyInjection;
using ExerciseTracker.Data;
using Microsoft.EntityFrameworkCore;
using ExerciseTracker.Repository;

namespace ExerciseTracker;

class Program
{
    static void Main(string[] args)
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
        builder.Services.AddSingleton(typeof(ExerciseRepository<>), typeof(ExerciseRepository<>));
        builder.Services.AddSingleton<ApplicationCoordinator>();

        // Build app from services
        var app = builder.Build();

        // Create a scope for DI management
        using var scope = app.Services.CreateScope();
        var appCoordinator = scope.ServiceProvider.GetRequiredService<ApplicationCoordinator>();

        appCoordinator.Start();
    }
}
