using Microsoft.AspNetCore.Mvc;

namespace CampusApi.Controllers;

[ApiController]
[Route("")]
public sealed class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetStatus() => Ok(new
    {
        servicio = "Campus API",
        estado = "disponible"
    });
}
