namespace LibraryManagementEFCORE.Models.DTOs
{
    public class AuthorProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class AuthorCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class AuthorUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
    public class AuthorChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
