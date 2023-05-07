using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);

        }

        public async Task<bool> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            return await Save();
        }

        public async Task<User> UpdateUserAsync(User entity)
        {
            var existingUser = await GetUserByIdAsync(entity.Id);
            if (existingUser != null)
                _context.Users.Update(entity);
            else
            {
                return null;
            }
            await Save();

            return existingUser;
        }
        public async Task<bool> DeleteUserAsync(User entity)
        {
            var existingUser = await EntityExistAsync(entity.Id);
            if (existingUser != null)
                _context.Users.Remove(entity);
            else
            {
                return false;
            }
            await Save();

            return existingUser;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> EntityExistAsync(int Id)
        {
            return await _context.Users.AnyAsync(x => x.Id == Id);
        }
    }
}
