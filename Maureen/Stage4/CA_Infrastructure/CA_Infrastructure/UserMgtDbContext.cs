using CA_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Infrastructure
{
	public class UserMgtDbContext : DbContext
	{
        public UserMgtDbContext(DbContextOptions<UserMgtDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
