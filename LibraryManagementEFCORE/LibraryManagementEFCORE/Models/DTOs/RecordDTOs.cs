namespace LibraryManagementEFCORE.Models.DTOs
{
    public class RecordProfileDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowedTime { get; set; }
        public DateTime ReturnedTime { get; set; }
    }
    public class RecordCreateDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowedTime { get; set; }
        public DateTime ReturnedTime { get; set; }
    }
    public class RecordUpdateDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowedTime { get; set; }
        public DateTime ReturnedTime { get; set; }
    }
}
