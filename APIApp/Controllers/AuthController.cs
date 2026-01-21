using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Services;

namespace APIApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthService service;

        public AuthController(AuthService service)
        {
            this.service = service;
        }

        [HttpPost("signup")]
        public IActionResult Signup(RegistrationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto.Role != "admin" && dto.Role != "patient" && dto.Role != "branchManager")
            {
                return BadRequest("Role must be either 'Shopkeeper' or 'Customer'");
            }

            var res = service.Register(dto);
            return res ? Ok("User Created") : BadRequest("User Not Created");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var token = service.Login(dto);
            if (token == null) return Unauthorized();
            return Ok("Log in Successfully: Heres your login Token: " + token);
        }
    }
}

