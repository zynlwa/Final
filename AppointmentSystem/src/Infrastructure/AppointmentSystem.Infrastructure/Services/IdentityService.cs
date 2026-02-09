
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystem.Infrastructure.Services;

public class IdentityService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IOptions<JwtSettings> jwtSettings,
    RoleManager<IdentityRole> roleManager,
    IAppDbContext context,
    IEmailService emailService) : IIdentityService
{
    private readonly JwtSettings jwtSettings = jwtSettings.Value;

    public async Task<Response<string>> RegisterPatientAsync(PatientRegisterDto dto)
    {
        var user = new AppUser
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.UserName,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return Response<string>.Fail(result.Errors.Select(e => e.Description), 400);

        if (!await roleManager.RoleExistsAsync(dto.Role))
            await roleManager.CreateAsync(new IdentityRole(dto.Role));

        await userManager.AddToRoleAsync(user, dto.Role);

        var patient = new Patient(
            firstName: user.FirstName,
            lastName: user.LastName,
            email: user.Email,
            phoneNumber: user.PhoneNumber,
            dateOfBirth: DateTime.UtcNow,
            appUserId: user.Id
        );
        context.Patients.Add(patient);
        await context.SaveChangesAsync();

        return Response<string>.Success(user.Id, 201);
    }

    public async Task<Response<string>> RegisterDoctorAsync(DoctorRegisterDto dto)
    {
        var user = new AppUser
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.UserName,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return Response<string>.Fail(result.Errors.Select(e => e.Description), 400);

        if (!await roleManager.RoleExistsAsync(dto.Role))
            await roleManager.CreateAsync(new IdentityRole(dto.Role));

        await userManager.AddToRoleAsync(user, dto.Role);

        var doctor = new Doctor(
            firstName: user.FirstName,
            lastName: user.LastName,
            email: user.Email,
            specialty: "Default Specialty",
            phoneNumber: user.PhoneNumber,
            experienceYears: dto.ExperienceYears,
            appUserId: user.Id
        );
        context.Doctors.Add(doctor);
        await context.SaveChangesAsync();

        return Response<string>.Success(user.Id, 201);
    }


    public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        // 1. İstifadəçini tap
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }

        // 2. Cari şifrəni yoxla və yeni şifrəni dəyiş
        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        return result;
    }

    public async Task<Response<LoginResponseDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            AppUser? user = null;

            // Email və ya username ilə tapırıq
            if (loginDto.EmailOrUsername!.Contains("@"))
                user = await userManager.Users
                            .Include(u => u.Doctor)
                            .Include(u => u.Patient)
                            .FirstOrDefaultAsync(u => u.Email == loginDto.EmailOrUsername);
            else
                user = await userManager.Users
                            .Include(u => u.Doctor)
                            .Include(u => u.Patient)
                            .FirstOrDefaultAsync(u => u.UserName == loginDto.EmailOrUsername);

            if (user == null || user.IsDeleted)
                return Response<LoginResponseDto>.Fail("Invalid email/username or password", 401);

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Response<LoginResponseDto>.Fail("Invalid email/username or password", 401);

            if (user.Doctor != null && user.MustChangePassword)
                return Response<LoginResponseDto>.Fail("You must change your temporary password", 403);

            // JWT token üçün
            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Patient";

            var token = await GenerateJwtToken(user, role);

            // UserDto üçün async map
            var userDto = await MapToUserDtoAsync(user);

            var loginResponse = new LoginResponseDto
            {
                Token = token,
                User = userDto,
                ExpiresAt = DateTime.UtcNow.AddHours(jwtSettings.ExpirationInHours)
            };

            return Response<LoginResponseDto>.Success(loginResponse, 200);
        }
        catch (Exception ex)
        {
            return Response<LoginResponseDto>.Fail($"An error occurred during login: {ex.Message}", 500);
        }
    }



    public async Task<Response<string>> LogoutAsync()
    {
        try
        {
            await signInManager.SignOutAsync();
            return Response<string>.Success("Logout successful", 200);
        }
        catch (Exception ex)
        {
            return Response<string>.Fail($"An error occurred during logout: {ex.Message}", 500);
        }
    }

    public async Task<Response<UserDto>> GetUserAsync(string userId)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return Response<UserDto>.Fail("User not found", 404);

            // User rolunu alırıq
            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User"; // default "User"

            // Doctor və Patient əlaqəsini tapırıq
            var doctor = await context.Doctors
                .FirstOrDefaultAsync(d => d.AppUserId == user.Id);

            var patient = await context.Patients
                .FirstOrDefaultAsync(p => p.AppUserId == user.Id);

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Role = role, // burda artıq role var
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DoctorId = doctor?.Id,
                PatientId = patient?.Id
            };

            return Response<UserDto>.Success(userDto, 200);
        }
        catch (Exception ex)
        {
            return Response<UserDto>.Fail($"An error occurred while retrieving user: {ex.Message}", 500);
        }
    }



    public async Task<Response<UserDto>> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return Response<UserDto>.Fail("User not found", 404);

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Response<UserDto>.Fail(errors, 400);
            }

            var userDto = await MapToUserDtoAsync(user);
            return Response<UserDto>.Success(userDto, 200);
        }
        catch (Exception ex)
        {
            return Response<UserDto>.Fail($"An error occurred while updating user: {ex.Message}", 500);
        }
    }

    public async Task<Response<string>> DeleteUserAsync(string userId)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return Response<string>.Fail("User not found", 404);

            // Soft delete
            user.SoftDelete(userId); // Using the userId as deletedBy for now

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Response<string>.Fail(errors, 400);
            }

            return Response<string>.Success("User deleted successfully", 200);
        }
        catch (Exception ex)
        {
            return Response<string>.Fail($"An error occurred while deleting user: {ex.Message}", 500);
        }
    }
    public async Task<Response<string>> ForgotPasswordAsync(ForgotPasswordDto dto)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Response<string>.Success("If the email is registered, you will receive a password reset link", 200);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"http://127.0.0.1:5501/pages/reset-password.html?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";


            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "emailResetTemplate.html");
            string html = await File.ReadAllTextAsync(templatePath);
            html = html.Replace("{{link}}", resetLink);
            html = html.Replace("{{username}}", user.UserName ?? "user");

           
            emailService.SendEmail(user.Email, "Reset your password", html);

            return Response<string>.Success($"Password reset link sent. For testing: {resetLink}", 200);
        }
        catch (Exception ex)
        {
            return Response<string>.Fail($"An error occurred while sending reset link: {ex.Message}", 500);
        }
    }
    public async Task<Response<string>> ResetPasswordAsync(ResetPasswordDto dto)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Response<string>.Fail("Invalid email", 404);

            var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Response<string>.Fail(errors, 400);
            }

            await userManager.UpdateSecurityStampAsync(user);
            return Response<string>.Success("Password has been reset successfully", 200);
        }
        catch (Exception ex)
        {
            return Response<string>.Fail($"An error occurred while resetting password: {ex.Message}", 500);
        }
    }
    public async Task<Response<string>> ChangeTemporaryPasswordAsync(string doctorEmail, string tempPassword, string newPassword)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(doctorEmail);
            if (user == null)
                return Response<string>.Fail("Doctor not found", 404);

            var isDoctor = await userManager.IsInRoleAsync(user, "Doctor");
            if (!isDoctor)
                return Response<string>.Fail("This action is allowed only for doctors", 403);

            var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, tempPassword, false);
            if (!passwordCheck.Succeeded)
                return Response<string>.Fail("Invalid temporary password", 401);

            
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Response<string>.Fail(errors, 400);
            }

            if (user is AppUser appUser)
                appUser.MustChangePassword = false;

            await userManager.UpdateAsync(user);

            return Response<string>.Success("Password changed successfully", 200);
        }
        catch (Exception ex)
        {
            return Response<string>.Fail($"An error occurred: {ex.Message}", 500);
        }
    }



    // IdentityService.cs
    private async Task<string> GenerateJwtToken(AppUser user, string role)
    {
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("role", role)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(jwtSettings.ExpirationInHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserDto> MapToUserDtoAsync(AppUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Patient";

        var doctor = await context.Doctors
            .FirstOrDefaultAsync(x => x.AppUserId == user.Id);

        var patient = await context.Patients
            .FirstOrDefaultAsync(x => x.AppUserId == user.Id);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            Role = role,
            DoctorId = doctor?.Id,   // ✔ doğru
            PatientId = patient?.Id  // ✔ doğru
        };
    }



}
public class JwtSettings
{
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpirationInHours { get; set; }
}