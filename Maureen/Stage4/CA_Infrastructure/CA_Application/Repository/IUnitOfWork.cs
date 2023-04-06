using CA_Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Application.Repository
{
	public interface IUnitOfWork
	{
        IUserRepository UserRepo { get; }
        int Save();
	}
}
