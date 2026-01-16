namespace LibraryManagementEFCORE.Models.Entities
{
    public class Book
    {
        //always remember that if you want to have a fk you should create a navigation
        public int Id { get; set; }
        public int AuthorId { get; set; }//FK
        public Author Author { get; set; } //Navigation
        public string Title { get; set; }
        public string References { get; set; }
        public bool IsAvailable { get; set; }
        //add this if this table is will be foriegn key to another table
        public ICollection<Record> Records { get; set; }
    }
}
