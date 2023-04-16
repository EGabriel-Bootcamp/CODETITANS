using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResponse> GetUsers();
        Task<ApiResponse> GetUser(int Id);
        Task<ApiResponse> CreateUser(User user);
        Task<ApiResponse> DeleteUsers(List<int> Ids);
        Task<ApiResponse> UpdateUser(User user);
        Task<ApiResponse> SearchUser(string searchKey);
    }
}
