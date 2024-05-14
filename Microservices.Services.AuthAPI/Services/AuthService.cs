using AutoMapper;
using Microservices.Services.AuthAPI.Data;
using Microservices.Services.AuthAPI.Models;
using Microservices.Services.AuthAPI.Models.Dtos.Login;
using Microservices.Services.AuthAPI.Models.Dtos.Register;
using Microservices.Services.AuthAPI.Models.Dtos.User;
using Microservices.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Microservices.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<UserExtended> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(AppDbContext context, UserManager<UserExtended> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IJwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var user = _context.UserExtended.FirstOrDefault(x => x.UserName.ToLower() == loginDto.Username.ToLower());
            var roles = await _userManager.GetRolesAsync(user);

            bool userValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (user == null || userValid == false)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = ""
                };
            }

            var token = _tokenGenerator.GenerateToken(user, roles);

            var userDto = _mapper.Map<UserDto>(user);
            var loginResponse = new LoginResponseDto
            {
                User = userDto,
                Token = token
            };

            return loginResponse;
        }

        public async Task<string> Register(RegisterRequestDto registerRequestDto)
        {
            UserExtended userExtended = new()
            {
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email,
                NormalizedEmail = registerRequestDto.Email,
                Name = registerRequestDto.Name,
                PhoneNumber = registerRequestDto.PhoneNumber
            };

            try
            {
                var resultRegister = await _userManager.CreateAsync(userExtended, registerRequestDto.Password);
                if (resultRegister.Succeeded)
                    return "";
                else
                {
                    var errrorMessageSb = new StringBuilder();
                    int errorCount = 1;
                    foreach (var error in resultRegister.Errors)
                    {
                        errrorMessageSb.Append($"{errorCount}: {error.Description} ");
                        errorCount++;
                    }
                    
                    return errrorMessageSb.ToString().Trim();
                }

            }
            catch (Exception ex)
            {
                return $"Erro encontrado: {ex.Message}";
            }
        }

        public async Task<bool> AddUserToRole(string email, string role)
        {
            var user = _context.UserExtended.FirstOrDefault(x => x.UserName.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();

                await _userManager.AddToRoleAsync(user, role);
                return true;
            }

            return false;
        }

    }
}
