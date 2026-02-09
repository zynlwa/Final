namespace AppointmentSystem.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<SoftDeleteInterceptor>();

        services.AddDbContext<AppDbContext>(
            (sp, options) => options
                .UseSqlServer(configuration.GetConnectionString("default")));
        //        .AddInterceptors(
        //            sp.GetRequiredService<SoftDeleteInterceptor>()));

        services.AddScoped<IAppDbContext, AppDbContext>();

        return services;
    }
}
