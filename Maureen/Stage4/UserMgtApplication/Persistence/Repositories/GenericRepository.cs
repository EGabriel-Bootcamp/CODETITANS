using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Application.Contracts.Persistence;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UserMgtDbContext _db;

        public GenericRepository(UserMgtDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(T entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _db.Set<T>();
            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
                return query.ToList();
            }
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _db.Set<T>();
            query = query.AsNoTracking().Where(filter);
            return await query.FirstOrDefaultAsync();
        }

        
    }
}
