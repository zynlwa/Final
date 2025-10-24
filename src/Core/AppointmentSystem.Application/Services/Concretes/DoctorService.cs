namespace AppointmentSystem.Application.Services.Concretes;

public class DoctorService(IAppDbContext context, IMapper mapper) : IDoctorService
{
    public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto createdoctorDto)
    {
        var existing = await context.Doctors
               .FirstOrDefaultAsync(d => d.FirstName == createdoctorDto.FirstName &&
                                         d.LastName == createdoctorDto.LastName &&
                                         !d.IsDeleted);

        if (existing != null)
            throw new ConflictException("Doctor already exists.");

        var doctor = mapper.Map<Doctor>(createdoctorDto);
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();
        var doctorDto = mapper.Map<DoctorDto>(doctor);

        return doctorDto;
    }

    public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
    {

        var doctors = await context.Doctors
            .Where(d => !d.IsDeleted)
            .ToListAsync();

        return mapper.Map<List<DoctorDto>>(doctors);

    }

    public async Task<DoctorDto> GetDoctorByIdAsync(string id)
    {
        var doctor = await context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);

        if (doctor == null)
            throw new NotFoundException("Doctor not found.");

        return mapper.Map<DoctorDto>(doctor);
    }

    public async Task SoftDeleteDoctorAsync(string id, string deletedBy)
    {
        var doctor = await context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);

        if (doctor == null)
            throw new NotFoundException("Doctor not found.");

        doctor.SoftDelete(deletedBy);

        context.Doctors.Update(doctor);
        await context.SaveChangesAsync();
    }


    public async Task UpdateDoctorAsync(string id, UpdateDoctorDto updateDoctorDto)
    {
        var doctor = await context.Doctors
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);

        if (doctor == null)
            throw new NotFoundException("Doctor not found.");

        doctor.UpdateDoctor(
            updateDoctorDto.FirstName,
            updateDoctorDto.LastName,
            updateDoctorDto.Email,
            updateDoctorDto.PhoneNumber,
            updateDoctorDto.Specialty
        );

        context.Doctors.Update(doctor);
        await context.SaveChangesAsync();
    }

}
