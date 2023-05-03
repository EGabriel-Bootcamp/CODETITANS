using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Domain;

namespace UserMgt.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task UpdateAsync(User user);

        Task<IEnumerable<User>> UserFilter(string searchText);

    }
}
