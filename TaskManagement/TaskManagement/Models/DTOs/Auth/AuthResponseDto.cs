namespace TaskManagement.Models.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public int ExpiresIn { get; set; }
        public UserDto User { get; set; } = null!;
         
    }
}
