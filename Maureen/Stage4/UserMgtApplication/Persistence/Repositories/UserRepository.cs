using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Contracts.Persistence;
using UserMgt.Domain;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserMgtDbContext _db;

        public UserRepository(UserMgtDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task UpdateAsync(User user)
        {
            var userFromDb = await _db.Users.FindAsync(user.Id);
            if (userFromDb != null)
            {
                userFromDb.FirstName = userFromDb.FirstName;
                userFromDb.DateOfBirth = userFromDb.DateOfBirth;
                userFromDb.Gender = userFromDb.Gender;
                if (user != null)
                {
                    userFromDb.LastName = user.LastName;
                    userFromDb.Address = user.Address;
                    userFromDb.City = user.City;
                    userFromDb.MaritalStatus = user.MaritalStatus;
                }
            }
            await _db.SaveChangesAsync();
        }


        public async Task<IEnumerable<User>> UserFilter(string searchText)
        {
            IQueryable<User> filtered = _db.Users.Where(x =>
                x.FirstName.Contains(searchText)
                || x.LastName.Contains(searchText)
                || x.MaritalStatus.Contains(searchText)
                || x.Gender.Contains(searchText)
                || x.Address.Contains(searchText)
                || x.City.Contains(searchText)
                || (DateTime.Now.Year - x.DateOfBirth.Year).ToString() == searchText

            );
            return await filtered.ToListAsync();
        }
    }
}
