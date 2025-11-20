namespace AppointmentSystem.Domain.Models
{
    public class Availability : BaseEntity
    {
        private Availability() { }

        public Availability(string doctorId, string medicalServiceId, DateTime startTime, DateTime endTime)
        {
            DoctorId = doctorId;
            MedicalServiceId = medicalServiceId;
            StartTime = startTime;
            EndTime = endTime;
            IsBooked = false;
        }

        public string DoctorId { get; private set; }
        public Doctor Doctor { get; private set; } = null!;

        public string MedicalServiceId { get; private set; }
        public MedicalService MedicalService { get; private set; } = null!; // navigation

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public bool IsBooked { get; private set; }

        public void Book() => IsBooked = true;
        public void Cancel() => IsBooked = false;

        public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    }
}
