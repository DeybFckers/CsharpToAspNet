using LibraryManagementEFCORE.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementEFCORE.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set;  }
        public DbSet<Record> Records { get; set; }
    }
}
