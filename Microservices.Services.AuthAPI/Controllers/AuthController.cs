using Microservices.Services.AuthAPI.Models.Dtos.Login;
using Microservices.Services.AuthAPI.Models.Dtos.Register;
using Microservices.Services.AuthAPI.Models.Dtos.Response;
using Microservices.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Microservices.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var errorMessage = await _authService.Register(registerRequestDto);

            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResult = await _authService.Login(loginDto);

            if(loginResult.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Usuário ou senha inválidos.";
                return BadRequest(_response);
            }

            _response.Result = loginResult;
            return Ok(loginResult);
        }

        [HttpPost("add-user-to-role/{email}/{role}")]
        public async Task<IActionResult> Add (string email, string role)
        {
            var addUserToRoleResult = await _authService.AddUserToRole(email, role);
            if(!addUserToRoleResult)
            {
                _response.IsSuccess = false;
                _response.Message = $"Falha ao atribuir a role ao usuário: {email}";
                return BadRequest(_response);
            }

            return Ok(_response);
        }
    }
}
