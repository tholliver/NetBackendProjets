using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        if (!await _service.AuthenticationService.ValidateUser(user))
            return Unauthorized();

        var tokenDto = await _service.AuthenticationService
                            .CreateToken(populateExp: true);

        return Ok(tokenDto);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string Id)
    {
        var result = await _service.AuthenticationService.DeleteUserById(Id);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return Ok(result);
    }
}
