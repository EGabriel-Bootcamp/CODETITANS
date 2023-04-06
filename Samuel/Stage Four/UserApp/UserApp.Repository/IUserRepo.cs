using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Repository
{
    public interface IUserRepo
    {

        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int Id);
        Task<bool> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User entity);
        Task<bool> DeleteUserAsync(User entity);
        Task<bool> EntityExistAsync(int Id);
    }
}
