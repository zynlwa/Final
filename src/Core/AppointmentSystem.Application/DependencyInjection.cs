namespace AppointmentSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<DoctorProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<PatientProfile>());
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        return services;
    }
}