namespace TaskManagement.Models.DTOs.Auth
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public UserFullnameDto fullname { get; set; }
        public string[] Roles { get; set; } = null!;

    }

    public class UserFullnameDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
    }

    public class AssignRoleDto
    {
        public string Email { get; set; } = null!;
        public string[] Roles { get; set; } = null!;
    }
}
