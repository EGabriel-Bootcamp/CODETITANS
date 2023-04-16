using Domain.Dtos;
using Domain.Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse> CreateUser(User user)
        {
            var newUser = await _repo.CreateUserAsync(user);
            return new ApiResponse() { Code = "00", Description = "Success", Data = newUser };
        }

        public async Task<ApiResponse> DeleteUsers(List<int> Ids)
        {
            var userExistsList = new List<User>();
            var userNotExists = new List<int>();
            foreach (var id in Ids)
            {
                var existingUser = await _repo.GetUserByIdAsync(id);
                if(existingUser != null) { userExistsList.Add(existingUser); } else { userNotExists.Add(id); }
            }

           if(userNotExists.Any())
                return new ApiResponse() { Code = "25", Description = "The following user does not eists", Data = userNotExists };


          await _repo.DeleteUsersAsync(userExistsList);

            return new ApiResponse() { Code = "00", Description = "Success", Data = null };

        }

        public async Task<ApiResponse> GetUser(int Id)
        {
            var user = await _repo.GetUserByIdAsync(Id);
            if (user == null) return new ApiResponse() { Code = "25", Description = "User not found", Data = null };

            return new ApiResponse() { Code = "00", Description = "Success", Data = user };

        }

        public async Task<ApiResponse> GetUsers()
        {
          var users = await _repo.GetUsersAsync();
          return new ApiResponse() { Code = "00", Description = "Success", Data = users };

        }

        public async Task<ApiResponse> SearchUser(string searchKey)
        {
          
            var users = await _repo.GetUsersAsync();
            var searchResult  = new List<User>();
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                searchResult = users;
            }
            else
            {
                var lowerCaseTerm = searchKey.Trim().ToLower();

                searchResult = users.Where(e => e.FirstName.ToLower().Contains(lowerCaseTerm)
                                    || e.LastName.ToLower().Contains(lowerCaseTerm)
                                    || e.Age.ToLower().Contains(lowerCaseTerm) || e.Location.ToLower().Contains(lowerCaseTerm)
                                    || e.MaritalStatus.ToLower().Contains(lowerCaseTerm)).ToList();


            }   


            return new ApiResponse() { Code = "00", Description = "Success", Data = searchResult };
        }

        public async Task<ApiResponse> UpdateUser(User user)
        {
          var existingUser = await _repo.GetUserByIdAsync(user.Id);
            if(existingUser== null) return new ApiResponse() { Code = "25", Description = "User not found", Data = null };

            existingUser.Age = user.Age;
            existingUser.Location = user.Location;
            existingUser.FirstName = user.FirstName;
            existingUser.Gender = user.Gender;
            existingUser.LastName = user.LastName;
            existingUser.MaritalStatus = user.MaritalStatus;
            await _repo.UpdateUserAsync(existingUser);

            return new ApiResponse() { Code = "00", Description = "Success", Data = user };

        }
    }
}
