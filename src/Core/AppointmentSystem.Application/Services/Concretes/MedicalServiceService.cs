namespace AppointmentSystem.Application.Services.Concretes;

public class MedicalServiceService(IAppDbContext context, IMapper mapper): IMedicalServiceService
{
    public async Task<MedicalServiceDto> CreateMedicalServiceAsync(CreateMedicalServiceDto dto)
    {
        var service = mapper.Map<MedicalService>(dto);
        context.MedicalServices.Add(service);
        await context.SaveChangesAsync();
        return mapper.Map<MedicalServiceDto>(service);
    }
    public async Task<IEnumerable<MedicalServiceDto>> GetAllMedicalServicesAsync()
    {
        var services = await context.MedicalServices.ToListAsync();
        return mapper.Map<IEnumerable<MedicalServiceDto>>(services);
    }

    public async Task<MedicalServiceDto?> GetMedicalServiceByIdAsync(string id)
    {
        var service = await context.MedicalServices.FirstOrDefaultAsync(s => s.Id == id);
        return service == null ? null : mapper.Map<MedicalServiceDto>(service);
    }

    public async Task<IEnumerable<MedicalServiceDto>> GetMedicalServicesForDoctorAsync(string doctorId)
    {
        var services = await context.MedicalServices
            .Where(s => s.DoctorId == doctorId)
            .ToListAsync();
        return mapper.Map<IEnumerable<MedicalServiceDto>>(services);
    }

    public async Task UpdateMedicalServiceAsync(string id, UpdateMedicalServiceDto dto)
    {
        var service = await context.MedicalServices.FirstOrDefaultAsync(s => s.Id == id);
        if (service == null)
            throw new NotFoundException("Medical service not found.");

        mapper.Map(dto, service);
        await context.SaveChangesAsync();
    }

    public async Task DeleteMedicalServiceAsync(string id)
    {
        var service = await context.MedicalServices.FirstOrDefaultAsync(s => s.Id == id);
        if (service == null)
            throw new NotFoundException("Medical service not found.");

        context.MedicalServices.Remove(service);
        await context.SaveChangesAsync();
    }
}
