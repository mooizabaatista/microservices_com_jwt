using Microservices.Services.AuthAPI.Models.Dtos.Login;
using Microservices.Services.AuthAPI.Models.Dtos.Register;

namespace Microservices.Services.AuthAPI.Services.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<bool> AddUserToRole(string email, string role);
    }
}
