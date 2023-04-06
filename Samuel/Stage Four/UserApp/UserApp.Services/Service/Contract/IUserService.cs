using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.Service.Contract
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetSingleUser(int Id);
        Task<bool> CreateUser(User entity);
        Task<User> DeleteUser(int Id);
        Task<User> UpdateUser(User entity);
        //Task<IQueryable<User>> Search(this IQueryable<User> users, string searchTerm);
        Task<List<User>> SearchUser(string searchTerm);
    }
}
