using Jolib.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jolib.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
       
    }
}
