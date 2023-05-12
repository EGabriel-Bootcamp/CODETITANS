using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            _context.User.Add(user);
            return await Save();
        }

        public async Task<bool> DeleteUsersAsync(List<User> users)
        {

            var deletedList = new List<User>();

            foreach (var user in users)
            {
                var userExists = await UserExistAsync(user.Id);
                if (userExists)
                {

                    _context.User.Remove(user);
                    deletedList.Add(user);
                }
            }


            await Save();

            return deletedList.Any();
        }

        public async Task<User?> GetUserByIdAsync(int Id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            var existingUser = await GetUserByIdAsync(user.Id);
            if (existingUser == null)
                return null;


            _context.User.Update(user);

            await Save();

            return existingUser;
        }

        public async Task<bool> UserExistAsync(int Id)
        {
            return await _context.User.AnyAsync(x => x.Id == Id);
        }
    }
}
