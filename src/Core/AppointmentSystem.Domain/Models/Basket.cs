namespace AppointmentSystem.Domain.Models;

public class Basket : BaseEntity
{
    private Basket() { }

    public Basket(string patientId)
    {
        PatientId = patientId;
        Items = new List<BasketItem>();
    }

    public string PatientId { get; private set; }
    public Patient Patient { get; private set; } = null!;

    public ICollection<BasketItem> Items { get; private set; }

    public void AddItem(string doctorId, string availabilityId, string medicalServiceId, decimal price)
    {
        if (Items.Any(x => x.AvailabilityId == availabilityId))
            throw new InvalidOperationException("This slot is already in the basket.");

        Items.Add(new BasketItem(doctorId, availabilityId, medicalServiceId, Id, price));
    }

    public void RemoveItem(string itemId)
    {
        var item = Items.FirstOrDefault(x => x.Id == itemId);
        if (item != null)
            Items.Remove(item);
    }
}

public class BasketItem : BaseEntity
{
    private BasketItem() { }

    public BasketItem(string doctorId, string availabilityId, string medicalServiceId, string basketId, decimal price)
    {
        DoctorId = doctorId;
        AvailabilityId = availabilityId;
        MedicalServiceId = medicalServiceId;
        BasketId = basketId;
        Price = price;
    }

    public string BasketId { get; private set; }
    public Basket Basket { get; private set; } = null!;
    public string DoctorId { get; private set; }
    public string AvailabilityId { get; private set; }
    public string MedicalServiceId { get; private set; }
    public decimal Price { get; private set; }
}
