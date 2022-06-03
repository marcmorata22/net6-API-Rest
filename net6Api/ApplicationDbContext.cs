using Microsoft.EntityFrameworkCore;
using net6Api.Models;

namespace net6Api
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
