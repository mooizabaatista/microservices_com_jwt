using Microservices.Services.AuthAPI.Models;

namespace Microservices.Services.AuthAPI.Services.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserExtended user, IEnumerable<string> roles);
    }
}
