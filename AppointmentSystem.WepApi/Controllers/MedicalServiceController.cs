using AppointmentSystem.Application.Common.Models.MedicalService;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalServiceController(IMedicalServiceService medicalService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await medicalService.GetAllMedicalServicesAsync();
        var response = Response<IEnumerable<MedicalServiceDto>>.Success(services, 200);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var service = await medicalService.GetMedicalServiceByIdAsync(id);
        if (service == null)
            return NotFound(Response<string>.Fail("Medical service not found.", 404));

        var response = Response<MedicalServiceDto>.Success(service, 200);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMedicalServiceDto dto)
    {
        var service = await medicalService.CreateMedicalServiceAsync(dto);
        var response = Response<MedicalServiceDto>.Success(service, 201);
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateMedicalServiceDto dto)
    {
        await medicalService.UpdateMedicalServiceAsync(id, dto);
        var response = Response<string>.Success("Medical service updated successfully.", 200);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await medicalService.DeleteMedicalServiceAsync(id);
        var response = Response<string>.Success("Medical service deleted successfully.", 200);
        return Ok(response);
    }
}
