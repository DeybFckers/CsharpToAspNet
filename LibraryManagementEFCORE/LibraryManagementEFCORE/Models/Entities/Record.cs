namespace LibraryManagementEFCORE.Models.Entities
{
    public class Record
    {
        //always remember that if you want to have a fk you should create a navigation
        public int Id { get; set; }
        public int BookId { get; set; }//Fk
        public Book Book { get; set; }//navigation
        public int MemberId { get; set; }//Fk
        public Member Member { get; set; }//Navigation
        public DateTime BorrowedTime { get; set; }
        public DateTime? ReturnedTime { get; set; }
    }
}
