using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Services.Service.Contract;

namespace UserApp.Services.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }


        public async Task<List<User>> GetAllUsers()
        {
            var usersList = await _repo.GetUsersAsync();
            if (usersList.Count < 0) throw new Exception("User Not Found");
            return usersList;
        }

        public async Task<User> GetSingleUser(int Id)
        {
            var user = await _repo.GetUserByIdAsync(Id);
            if (user == null) throw new Exception("User Not Found");
            return user;
        }

        public async Task<bool> CreateUser(User entity)
        {
            var newUser = await _repo.CreateUserAsync(entity);
            return newUser;
        }

        public async Task<User> DeleteUser(int Id)
        {
            var existingUser = await _repo.GetUserByIdAsync(Id);

            await _repo.DeleteUserAsync(existingUser);
            return existingUser;
        }

        public async Task<User> UpdateUser(User entity)
        {

            var existingUser = await _repo.GetUserByIdAsync(entity.Id);

            existingUser.Age = entity.Age;
            existingUser.Country = entity.Country;
            existingUser.FirstName = entity.FirstName;
            existingUser.Gender = entity.Gender;
            existingUser.LastName = entity.LastName;
            existingUser.MaritalStatus = entity.MaritalStatus;

            await _repo.UpdateUserAsync(existingUser);

            return existingUser;
        }

        public async Task<List<User>> SearchUser(string searchTerm)
        {
            var users = await _repo.GetUsersAsync();
            if (string.IsNullOrWhiteSpace(searchTerm))
                return users;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return users.Where(e => e.FirstName.ToLower().Contains(lowerCaseTerm)
                                || e.LastName.ToLower().Contains(lowerCaseTerm)
                                || e.Age.ToLower().Contains(lowerCaseTerm)
                                || e.MaritalStatus.ToLower().Contains(lowerCaseTerm)).ToList();

        }
    }
}
