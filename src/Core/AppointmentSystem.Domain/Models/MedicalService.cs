namespace AppointmentSystem.Domain.Entities;

public class MedicalService : BaseEntity
{
    private MedicalService() { }

    public MedicalService(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

    public string DoctorId { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
}
