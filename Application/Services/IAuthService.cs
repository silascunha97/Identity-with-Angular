
using Identity.Application.DTOs;

namespace Identity.Application.Services
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto loginDto);
        Task<string> Register(RegisterDto registerDto);
    }
}
