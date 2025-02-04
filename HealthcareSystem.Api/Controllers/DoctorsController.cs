using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Controllers;

public record GetDoctorsRequest(
    int? PageIndex, int? PageSize, string? SortField,
    string? SortOrder, string? SearchField, string? SearchValue
);

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase {
    private readonly IDoctorRepository _doctorRepository;

    public DoctorsController(IDoctorRepository doctorRepository) {
        _doctorRepository = doctorRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetDoctors(
        [FromQuery] GetDoctorsRequest request, CancellationToken ct
    ) {
        ICollection<Doctor>? list = await _doctorRepository.GetDoctors(
            request.PageIndex, request.PageSize, request.SortField,
            request.SortOrder, request.SearchField, request.SearchValue
        );
        if (list is null) {
            return NotFound();
        }
        return Ok(list);
    }
}