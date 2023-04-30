using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Identity.Models;

namespace UserMgt.Identity.DbContext
{
    public class UserMgtIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserMgtIdentityDbContext(DbContextOptions<UserMgtIdentityDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(UserMgtIdentityDbContext).Assembly);
        }
    }
}
