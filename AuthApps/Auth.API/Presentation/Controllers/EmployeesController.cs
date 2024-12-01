using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Manager")]
    public IActionResult GetEmployees()
    {
        Console.WriteLine("Running here");
        return Ok("Hello there");
    }
}
