namespace AppointmentSystem.Application.Validators.AppointmentValidators;

public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
{
    public CreateAppointmentDtoValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("Doctor ID is required.");

        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("Patient ID is required.");

        RuleFor(x => x.AvailabilityId)
            .NotEmpty().WithMessage("Availability ID is required.");

        RuleFor(x => x.MedicalServiceId)
            .NotEmpty().WithMessage("Medical Service ID is required.");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Notes))
            .WithMessage("Notes cannot exceed 500 characters.");
    }
}
