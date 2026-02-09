using AppointmentSystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAdmin()
        {
            // Əgər Admin rolu yoxdursa, yarad
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            // Admin məlumatı
            var adminUser = new AppUser
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
                FirstName = "System",
                LastName = "Admin"
            };

            // İstifadəçini yarat
            var result = await _userManager.CreateAsync(adminUser, "Admin123!");

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Rol əlavə et
            await _userManager.AddToRoleAsync(adminUser, "Admin");

            return Ok("Admin created successfully!");
        }
    }
}

