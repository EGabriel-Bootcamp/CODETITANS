using CA_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Application.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		void Update(User user);
	}
}
