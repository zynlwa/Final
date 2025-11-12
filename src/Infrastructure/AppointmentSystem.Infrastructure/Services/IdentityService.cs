using AppointmentSystem.Application.Common.Interfaces;
using AppointmentSystem.Application.Common.Models.Identity;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppointmentSystem.Infrastructure.Services;

public class IdentityService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IOptions<JwtSettings> jwtSettings,
    RoleManager<IdentityRole> roleManager,
    IAppDbContext context) : IIdentityService
{
    private readonly JwtSettings jwtSettings = jwtSettings.Value;

    public async Task<Response<string>> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            // 1️⃣ Email / Username yoxlama
            var existingUserByEmail = await userManager.FindByEmailAsync(registerDto.Email);
            if (existingUserByEmail != null)
                return Response<string>.Fail("User with this email already exists", 400);

            var existingUserByUsername = await userManager.FindByNameAsync(registerDto.UserName);
            if (existingUserByUsername != null)
                return Response<string>.Fail("User with this username already exists", 400);

            // 2️⃣ AppUser yarat
            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Response<string>.Fail(errors, 400);
            }

            // 3️⃣ Role yoxdursa yarat
            if (!await roleManager.RoleExistsAsync(registerDto.Role))
                await roleManager.CreateAsync(new IdentityRole(registerDto.Role));

            // 4️⃣ User-i role əlavə et
            await userManager.AddToRoleAsync(user, registerDto.Role);

            // 5️⃣ Rol-a görə entity əlavə et (constructor-based)
            switch (registerDto.Role)
            {
                case "Doctor":
                    var doctor = new Doctor(
                        firstName: user.FirstName,
                        lastName: user.LastName,
                        email: user.Email,
                        specialty: "Default Specialty", // lazım olsa frontend-dən al
                        phoneNumber: user.PhoneNumber,
                        appUserId: user.Id
                    );
                    context.Doctors.Add(doctor);
                    break;

                case "Patient":
                    var patient = new Patient(
                        firstName: user.FirstName,
                        lastName: user.LastName,
                        email: user.Email,
                        phoneNumber: user.PhoneNumber,
                        dateOfBirth: DateTime.UtcNow, // frontend-dən almalı
                        appUserId: user.Id
                    );
                    context.Patients.Add(patient);
                    break;

                case "Admin":
                    // Admin üçün ayrıca entity lazım deyil
                    break;
            }

            await context.SaveChangesAsync();

            return Response<string>.Success(user.Id, 201);
        }
        catch (Exception ex)
        {
            return Response<string>.Fail($"An error occurred during registration: {ex.Message}", 500);
        }
    }



    public async Task<Response<LoginResponseDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            // İstifadəçi həm email, həm də username ilə daxil ola bilər
            AppUser? user = null;

            if (loginDto.EmailOrUsername!.Contains("@"))
                user = await userManager.FindByEmailAsync(loginDto.EmailOrUsername);
            else
                user = await userManager.FindByNameAsync(loginDto.EmailOrUsername);

            if (user == null)
                return Response<LoginResponseDto>.Fail("Invalid email/username or password", 401);

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
                return Response<LoginResponseDto>.Fail("Invalid email/username or password", 401);

            var token = await GenerateJwtToken(user);
            var userDto = MapToUserDto(user);

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

            var userDto = MapToUserDto(user);
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

            var userDto = MapToUserDto(user);
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

    // IdentityService.cs
    private async Task<string> GenerateJwtToken(AppUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? string.Empty;

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(ClaimTypes.NameIdentifier, user.Id), // <-- Əlavə edildi
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


    private static UserDto MapToUserDto(AppUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
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