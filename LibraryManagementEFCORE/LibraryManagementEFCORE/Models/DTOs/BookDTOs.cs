namespace LibraryManagementEFCORE.Models.DTOs
{
    public class BookCreateDto
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string References { get; set; }
        public bool IsAvailable { get; set; } = false;
    }
    public class BookProfileDto
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string References { get; set; }
        public bool IsAvailable { get; set; }
    }
    public class BookUpdateDto
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string References { get; set; }
        public bool IsAvailable { get; set; }
    }
}
