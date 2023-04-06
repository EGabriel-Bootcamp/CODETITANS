using CA_Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Infrastructure.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly UserMgtDbContext _db;
        public UnitOfWork(UserMgtDbContext db)
        {
			_db = db;
			UserRepo = new UserRepository(db);
        }
        public IUserRepository UserRepo { get; private set; }

		public int Save()
		{
			return _db.SaveChanges();
		}
	}
}
