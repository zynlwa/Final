namespace AppointmentSystem.Application.Validators.AvailabilityValidators;

public class CreateAvailabilityDtoValidator : AbstractValidator<CreateAvailabilityDto>
{
    public CreateAvailabilityDtoValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("Doctor ID is required.");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.Now).WithMessage("Start time must be in the future.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be after start time.");
    }
}
