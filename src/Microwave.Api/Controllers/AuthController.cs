using Microsoft.AspNetCore.Mvc;
using Microwave.Application.Contracts.Requests;
using Microwave.Application.UseCases;
using LoginRequest = Microwave.Application.Contracts.Requests.LoginRequest;

namespace Microwave.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] ILoginUseCase loginUseCase)
    {
        var response = await loginUseCase.LoginAsync(request);
        
        return Ok(response);
        
        //return Unauthorized();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] IRegisterUseCase useCase)
    {
        var response = await useCase.RegisterAsync(request);
        
        return Ok(response);
        
        return Unauthorized();
    }
}
