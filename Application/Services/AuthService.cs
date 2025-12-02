// Application/Services/AuthService.cs
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityWithAngular.Application.DTOs;
using IdentityWithAngular.Application.Services;
using IdentityWithAngular.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityWithAngular.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = "Nome Padrão", // Substitua por valor real se disponível
                RegistrationNumber = "MatriculaPadrão", // Substitua por valor real se disponível
                Department = "DepartamentoPadrão" // Substitua por valor real se disponível
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                // TODO: Melhorar o tratamento de erro, talvez retornando os erros do Identity.
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed: {errors}");
            }
            return await GenerateToken(user);
        }

        public async Task<string> Login(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new Exception("Login failed. Check credentials.");
            }
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                // Isso não deveria acontecer se o PasswordSignInAsync teve sucesso, mas é uma boa verificação.
                throw new Exception("User not found after successful login.");
            }
            return await GenerateToken(user);
        }

        private Task<string> GenerateToken(ApplicationUser user)
        {
            var email = user.Email ?? throw new ArgumentNullException(nameof(user.Email), "User email cannot be null.");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // TODO: Adicionar outras claims relevantes (roles, etc.)
            };

            var jwtKey = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("JWT key (Jwt:Key) is not configured in appsettings.");
                
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
