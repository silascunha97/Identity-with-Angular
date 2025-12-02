
using IdentityWithAngular.Application.DTOs;

namespace IdentityWithAngular.Application.Services
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto loginDto);
        Task<string> Register(RegisterDto registerDto);
    }
}
