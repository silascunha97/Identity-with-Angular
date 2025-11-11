using Identity.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Identity.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Identity.Application.Services;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Identity.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        // 🔓 Endpoint público para registro
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Regra de negócio: Verifica se o usuário já existe
            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
            {

                return BadRequest(new { message = "User with this email already exists." });

                //return Conflict(new { message = "User with this email already exists." });
            }
            try
            {
                var token = await _authService.Register(dto);
                //return Ok(new { token });
                return Created(new { token });
            }
            catch (Exception ex)
            {
                // Melhorar o tratamento de erro conforme necessário

                return BadRequest(new { message = ex.Message });
                
            }
        }

        private IActionResult Created(object value)
        {
            return StatusCode(201, value);
        }

        // 🔓 Endpoint público para login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var token = await _authService.Login(dto);
                return Ok(new { token });
            }
            catch (Exception)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
        }

        // 🔐 Endpoint protegido para listar usuários
        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(u => new
            {
                u.Id,
                u.Email,
                u.UserName,
                u.FullName,
                u.RegistrationNumber,
                u.Department
            }).ToList();

            return Ok(users);
        }
    }
}
