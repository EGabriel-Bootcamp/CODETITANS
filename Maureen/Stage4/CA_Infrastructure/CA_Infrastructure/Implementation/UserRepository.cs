using CA_Application.Repository;
using CA_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Infrastructure.Implementation
{
	public class UserRepository : Repository<User>, IUserRepository
	{
        private readonly UserMgtDbContext _db;
        public UserRepository(UserMgtDbContext db) :base(db)
        {
            _db = db;
        }

		public void Update(User user)
		{
			_db.Users.Update(user);
		}
	}
}
