using AppointmentSystem.Application.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure JWT settings
        services.Configure<Services.JwtSettings>(options =>
        {
            options.SecretKey = configuration["JwtSettings:SecretKey"]!;
            options.Issuer = configuration["JwtSettings:Issuer"]!;
            options.Audience = configuration["JwtSettings:Audience"]!;
            options.ExpirationInHours = int.Parse(configuration["JwtSettings:ExpirationInHours"]!);
        });

        // Register Identity service
        services.AddScoped<IIdentityService, Services.IdentityService>();

        return services;
    }
}
