namespace AppointmentSystem.Domain.Exceptions;

public class ConflictException(string message) : BaseException(message, 409)
{
}
