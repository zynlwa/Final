using Microsoft.AspNetCore.Identity;

namespace AppointmentSystem.Application.Services.Concretes;

public class DoctorService(
    IAppDbContext context,
    IMapper mapper,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager
) : IDoctorService
{
    public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto createDoctorDto)
    {
        var existingUser = await userManager.FindByEmailAsync(createDoctorDto.Email);
        if (existingUser != null) throw new ConflictException("A user with this email already exists.");

        var existingDoctor = await context.Doctors
            .FirstOrDefaultAsync(d => d.Email == createDoctorDto.Email && !d.IsDeleted);
        if (existingDoctor != null) throw new ConflictException("A doctor with this email already exists.");

        var appUser = new AppUser
        {
            FirstName = createDoctorDto.FirstName,
            LastName = createDoctorDto.LastName,
            UserName = createDoctorDto.Email,
            Email = createDoctorDto.Email
        };

        var result = await userManager.CreateAsync(appUser, createDoctorDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User creation failed: {errors}");
        }

        if (!await roleManager.RoleExistsAsync("Doctor"))
            await roleManager.CreateAsync(new IdentityRole("Doctor"));

        await userManager.AddToRoleAsync(appUser, "Doctor");

        var doctor = new Doctor(
            createDoctorDto.FirstName,
            createDoctorDto.LastName,
            createDoctorDto.Email,
            createDoctorDto.Specialty,
            createDoctorDto.PhoneNumber,
            appUser.Id,
            createDoctorDto.ImageUrl
        );

        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();

        return mapper.Map<DoctorDto>(doctor);
    }

    public async Task UpdateDoctorAsync(string id, UpdateDoctorDto updateDoctorDto)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        if (doctor == null) throw new NotFoundException("Doctor not found.");

        if (!string.Equals(doctor.Email, updateDoctorDto.Email, StringComparison.OrdinalIgnoreCase))
        {
            var existingUser = await userManager.FindByEmailAsync(updateDoctorDto.Email);
            if (existingUser != null && existingUser.Id != doctor.AppUserId)
                throw new ConflictException("This email is already in use by another user.");
        }

        doctor.Update(
            updateDoctorDto.FirstName,
            updateDoctorDto.LastName,
            updateDoctorDto.Email,
            updateDoctorDto.PhoneNumber,
            updateDoctorDto.Specialty,
            updateDoctorDto.ImageUrl
        );

        var appUser = await userManager.FindByIdAsync(doctor.AppUserId);
        if (appUser != null && appUser.Email != updateDoctorDto.Email)
        {
            appUser.Email = updateDoctorDto.Email;
            appUser.UserName = updateDoctorDto.Email;
            appUser.FirstName = updateDoctorDto.FirstName;
            appUser.LastName = updateDoctorDto.LastName;

            await userManager.UpdateAsync(appUser);
        }

        context.Doctors.Update(doctor);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
    {
        var doctors = await context.Doctors.Where(d => !d.IsDeleted).ToListAsync();
        return mapper.Map<List<DoctorDto>>(doctors);
    }

    public async Task<DoctorDto> GetDoctorByIdAsync(string id)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        if (doctor == null) throw new NotFoundException("Doctor not found.");
        return mapper.Map<DoctorDto>(doctor);
    }

    public async Task SoftDeleteDoctorAsync(string id, string deletedBy)
    {
        var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        if (doctor == null) throw new NotFoundException("Doctor not found.");

        doctor.SoftDelete(deletedBy);
        context.Doctors.Update(doctor);
        await context.SaveChangesAsync();
    }
}
