using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Services;

namespace SistemaVeterinario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] UserLoginAux model)
    {
        // Aqui você pode validar com seu banco ou manter fixo para o teste do professor/challenge
        if (model.Username == "admin" && model.Password == "123456")
        {
            var token = _tokenService.GenerateToken(model.Username);
            return Ok(new { token });
        }
        return Unauthorized(new { message = "Usuário ou senha inválidos" });
    }

    public class UserLoginAux
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}