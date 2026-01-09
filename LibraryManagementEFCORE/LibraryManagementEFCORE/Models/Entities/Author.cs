namespace LibraryManagementEFCORE.Models.Entities
{
    public class Author
    {
        //always remember that if you want to have a fk you should create a navigation
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //add this if this table is will be foriegn key to another table
        public ICollection<Book> Books { get; set; }
    }

}
