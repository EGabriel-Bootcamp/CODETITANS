using Domain.Dtos;
using Domain.Entities;
using Services.CacheRepository;

namespace Services.UserServices
{
    public interface IUserService : ICacheRepository<List<User>>
    {
        Task<ApiResponse> GetUsers();
        Task<ApiResponse> GetUser(int Id);
        Task<ApiResponse> CreateUser(User user);
        Task<ApiResponse> DeleteUsers(List<int> Ids);
        Task<ApiResponse> UpdateUser(User user);
        Task<ApiResponse> SearchUser(string searchKey);
    }
}
