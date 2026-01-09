namespace LibraryManagementEFCORE.Models.DTOs
{
    public class MemberProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class MemberCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class MemberUpdateDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
    public class MemberChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
