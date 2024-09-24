using System.Text.Json;
using API.Middleware;
using DataAccess;
using DataAccess.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service;
using Service.Validators;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure AppOptions with validation
        builder.Services.AddOptionsWithValidateOnStart<AppOptions>()
            .Bind(builder.Configuration.GetSection(nameof(AppOptions)))
            .ValidateDataAnnotations()
            .Validate(options => new AppOptionsValidator().Validate(options).IsValid,
                $"{nameof(AppOptions)} validation failed");

        // Configure DbContext for the webshop
        builder.Services.AddDbContext<WebshopContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(appOptions.DbConnectionString);  // Assuming PostgreSQL is used
            options.EnableSensitiveDataLogging(); // Enable for debugging, turn off in production
        });
    
        // Register Fluent Validation (replace with your validators)
        builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateOrderValidator>());

        // Register generic repository for dependency injection
        builder.Services.AddScoped(typeof(IRepository<>), typeof(WebshopRepository<>));

        // Register any other necessary services (e.g., OrderService, PaperService)
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IPaperService, PaperService>();

        // Add controllers and OpenAPI (Swagger)
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument();

        var app = builder.Build();

        // Retrieve AppOptions and display in the console
        var options = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;
        Console.WriteLine("APP OPTIONS: " + JsonSerializer.Serialize(options));

        app.UseHttpsRedirection();
        app.UseRouting();

        // Add OpenAPI (Swagger) and middleware
        app.UseOpenApi();
        app.UseSwaggerUi();
        app.UseStatusCodePages();

        // Enable CORS (for development)
        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        // Ensure the database is created at startup (can be removed for production)
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<WebshopContext>();
            context.Database.EnsureCreated();  // Make sure DB schema is applied
        }

        // Run the application
        app.Run();
    }
    
}