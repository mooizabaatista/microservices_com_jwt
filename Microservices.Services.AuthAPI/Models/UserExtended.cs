using Microsoft.AspNetCore.Identity;

namespace Microservices.Services.AuthAPI.Models
{
    public class UserExtended : IdentityUser
    {
        public string? Name { get; set; }
    }
}
