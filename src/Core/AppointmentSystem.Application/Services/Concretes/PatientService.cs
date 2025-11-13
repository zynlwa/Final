using Microsoft.AspNetCore.Identity;
namespace AppointmentSystem.Application.Services.Concretes;

public class PatientService(IAppDbContext context, IMapper mapper, UserManager<AppUser> userManager) : IPatientService
{
    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto)
    {
       
        var existingUser = await userManager.FindByEmailAsync(patientDto.Email);
        if (existingUser != null)
            throw new ConflictException("A user with this email already exists.");

        var existingPatient = await context.Patients
            .FirstOrDefaultAsync(p => p.Email == patientDto.Email && !p.IsDeleted);

        if (existingPatient != null)
            throw new ConflictException("Patient with this email already exists.");

      
        var appUser = new AppUser
        {
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Email = patientDto.Email,
            UserName = patientDto.Email,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(appUser, patientDto.Password);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

     
        await userManager.AddToRoleAsync(appUser, "Patient");

      
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

        
        if (!string.Equals(patient.Email, patientDto.Email, StringComparison.OrdinalIgnoreCase))
        {
            var existingUser = await userManager.FindByEmailAsync(patientDto.Email);
            if (existingUser != null && existingUser.Id != patient.AppUserId)
                throw new ConflictException("This email is already in use by another user.");
        }

        patient.Update(
            patientDto.FirstName,
            patientDto.LastName,
            patientDto.Email,
            patientDto.PhoneNumber,
            patientDto.DateOfBirth
        );

        
        var appUser = await userManager.FindByIdAsync(patient.AppUserId);
        if (appUser != null && appUser.Email != patientDto.Email)
        {
            appUser.Email = patientDto.Email;
            appUser.UserName = patientDto.Email;
            appUser.FirstName = patientDto.FirstName;
            appUser.LastName = patientDto.LastName;

            await userManager.UpdateAsync(appUser);
        }

        context.Patients.Update(patient);
        await context.SaveChangesAsync();
    }

    public async Task SoftDeletePatientAsync(string id, string deletedBy)
    {
        var patient = await context.Patients
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (patient == null)
            throw new NotFoundException("Patient not found.");

        patient.SoftDelete(deletedBy);
        context.Patients.Update(patient);

        await context.SaveChangesAsync();
    }
}
