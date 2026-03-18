namespace TaskManagement.Models.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; } = null!;
        public UserDto User { get; set; } = null!;
         
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = null!;
    }
}
