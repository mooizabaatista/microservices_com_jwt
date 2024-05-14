namespace Microservices.Services.AuthAPI.Models.Dtos.Jwt
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audiance { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
