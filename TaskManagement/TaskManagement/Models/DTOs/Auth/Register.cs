using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models.DTOs.Auth
{
    public class Register
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Valid First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please Enter Valid Last Name")]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
