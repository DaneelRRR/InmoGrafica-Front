using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases.Usuarios;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RegistrarUsuarioInteractor _registrarInteractor;
        private readonly LoginInteractor _loginInteractor;

        public AuthController(RegistrarUsuarioInteractor registrarInteractor, LoginInteractor loginInteractor)
        {
            _registrarInteractor = registrarInteractor;
            _loginInteractor = loginInteractor;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var usuario = await _registrarInteractor.Handle(dto);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _loginInteractor.Handle(dto);
            if (usuario == null) return Unauthorized("Credenciales inválidas");

            return Ok(new { Usuario = usuario, Token = "token_falso_para_pruebas" });
        }
    }
}
