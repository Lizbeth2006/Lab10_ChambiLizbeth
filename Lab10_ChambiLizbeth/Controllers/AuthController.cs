using System.Security.Claims;
using Lab10_ChambiLizbeth.Application.DTOs.Users;
using Lab10_ChambiLizbeth.Application.UseCases.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_ChambiLizbeth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserUseCase _registerUser;
    private readonly LoginUseCase _login;

    public AuthController(RegisterUserUseCase registerUser, LoginUseCase login)
    {
        _registerUser = registerUser;
        _login = login;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserResponse>> Register(RegisterUserRequest request)
    {
        var result = await _registerUser.ExecuteAsync(request);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var result = await _login.ExecuteAsync(request);
        return Ok(result);
    }

    // Endpoint protegido: sirve para comprobar que el token funciona
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            username = User.Identity?.Name,
            roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value)
        });
    }
}   