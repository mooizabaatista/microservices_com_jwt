namespace Microservices.Services.AuthAPI.Models.Dtos.Response
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public string? Token { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; } = string.Empty;
    }
}
