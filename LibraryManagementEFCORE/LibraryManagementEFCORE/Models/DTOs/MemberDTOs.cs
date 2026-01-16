using System.ComponentModel.DataAnnotations;

namespace LibraryManagementEFCORE.Models.DTOs
{
    public class MemberProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class MemberCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
    public class MemberUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class MemberChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class MemberLoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class MemberTokenDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
