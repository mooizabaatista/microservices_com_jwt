namespace Microservices.Services.AuthAPI.Models.Dtos.Register
{
    public class RegisterRequestDto
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
