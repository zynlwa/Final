namespace AppointmentSystem.Application.Services.Abstractions;
public interface IIdentityService
{
    Task<Response<string>> RegisterAsync(RegisterDto registerDto);
    Task<Response<LoginResponseDto>> LoginAsync(LoginDto loginDto);
    Task<Response<string>> LogoutAsync();
    Task<Response<UserDto>> GetUserAsync(string userId);
    Task<Response<UserDto>> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
    Task<Response<string>> DeleteUserAsync(string userId);
    Task<Response<string>> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task<Response<string>> ResetPasswordAsync(ResetPasswordDto dto);

}
