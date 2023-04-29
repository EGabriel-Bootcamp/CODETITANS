using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Domain;

namespace Persistence
{
    public class UserMgtDbContext : DbContext
    {
        public UserMgtDbContext(DbContextOptions<UserMgtDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
