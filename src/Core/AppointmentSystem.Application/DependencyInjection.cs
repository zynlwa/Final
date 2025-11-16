using System.Reflection;

namespace AppointmentSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddAutoMapper(cfg => cfg.AddProfile<DoctorProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<PatientProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<AppointmentProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<AvailabilityProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<MedicalServiceProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<BasketProfile>());
        services.AddAutoMapper(cfg => cfg.AddProfile<PromoCodeProfile>());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddScoped<IMedicalServiceService, MedicalServiceService>();
        services.AddScoped<IBasketService, BasketService>();
        return services;
    }
}
