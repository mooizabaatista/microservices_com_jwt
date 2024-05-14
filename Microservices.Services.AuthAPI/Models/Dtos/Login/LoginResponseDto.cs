using Microservices.Services.AuthAPI.Models.Dtos.User;

namespace Microservices.Services.AuthAPI.Models.Dtos.Login
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
    }
}
