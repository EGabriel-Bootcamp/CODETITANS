using CA_Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Application.Services
{
	public interface IUserService : IUserRepository
	{
		void Save();
	}
}
