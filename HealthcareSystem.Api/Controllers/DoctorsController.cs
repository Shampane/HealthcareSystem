using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Doctors";
    }
}