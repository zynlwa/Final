
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Application.Services.Concretes;

public class PatientService(IAppDbContext context, IMapper mapper,UserManager<AppUser> userManager) : IPatientService
{
    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto)
    {
        // Email yoxlaması
        var existing = await context.Patients
            .FirstOrDefaultAsync(p => p.Email == patientDto.Email && !p.IsDeleted);

        if (existing != null)
            throw new ConflictException("Patient with this email already exists.");

        // AppUser yaradılır
        var appUser = new AppUser
        {
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Email = patientDto.Email,
            UserName = patientDto.Email, // Email username kimi
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(appUser, patientDto.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        // AppUser role təyin etmək (optional)
        await userManager.AddToRoleAsync(appUser, "Patient");

        // Patient entity yaradılır
        var patient = new Patient(
            patientDto.FirstName,
            patientDto.LastName,
            patientDto.Email,
            patientDto.PhoneNumber,
            patientDto.DateOfBirth,
            appUser.Id
        );

        await context.Patients.AddAsync(patient);
        await context.SaveChangesAsync();

        return mapper.Map<PatientDto>(patient);
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
    {
        var patients = await context.Patients
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        return mapper.Map<List<PatientDto>>(patients);
    }

    public async Task<PatientDto> GetPatientByIdAsync(string id)
    {
        var patient = await context.Patients
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (patient == null)
            throw new NotFoundException("Patient not found.");

        return mapper.Map<PatientDto>(patient);
    }

    public async Task UpdatePatientAsync(string id, UpdatePatientDto patientDto)
    {
        var patient = await context.Patients
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (patient == null)
            throw new NotFoundException("Patient not found.");

        patient.Update(
            patientDto.FirstName,
            patientDto.LastName,
            patientDto.Email,
            patientDto.PhoneNumber,
            patientDto.DateOfBirth
        );

        context.Patients.Update(patient);
        await context.SaveChangesAsync();
    }

    public async Task SoftDeletePatientAsync(string id, string deletedBy)
    {
        var patient = await context.Patients
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (patient == null)
            throw new NotFoundException("Patient not found.");

        // Soft delete
        patient.SoftDelete(deletedBy);
        context.Patients.Update(patient);

        await context.SaveChangesAsync();
    }
}
