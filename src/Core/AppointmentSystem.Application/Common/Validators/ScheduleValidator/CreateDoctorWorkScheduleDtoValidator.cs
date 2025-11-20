using AppointmentSystem.Application.Common.Models.DoctorSchedule;

namespace AppointmentSystem.Application.Common.Validators.ScheduleValidator;

public class CreateDoctorWorkScheduleDtoValidator : AbstractValidator<CreateWorkScheduleDto>
{
    public CreateDoctorWorkScheduleDtoValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("Doctor ID is required.");

        RuleFor(x => x.DayOfWeek)
    .Must(d => d >= DayOfWeek.Sunday && d <= DayOfWeek.Saturday)
    .WithMessage("DayOfWeek must be between Sunday and Saturday.");


        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
    }
}

