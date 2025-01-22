using System;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Controllers;

[Route("api/token")]
[ApiController]
public class TokenController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenDto refreshTokenDto)
    {
        if (refreshTokenDto is null)
            return BadRequest(new { message = "Invalid request" });

        var token = await _service.AuthenticationService.RefreshToken(refreshTokenDto);
        if (token is null)
            return BadRequest(new { message = "Invalid token" });

        return Ok(token);
    }

}
