using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace AppointmentSystem.Application.Services.Concretes;

public class CurrentUserService : ICurrentUserService
{
    public string UserId { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }
}
