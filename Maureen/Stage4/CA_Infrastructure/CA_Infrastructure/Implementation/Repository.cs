using CA_Application.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CA_Infrastructure.Implementation
{
	public class Repository<T> : IRepository<T> where T : class
	{
        private readonly UserMgtDbContext _db;
		public DbSet<T> dbSet;
        public Repository(UserMgtDbContext db)
        {
            _db = db;
			this.dbSet = _db.Set<T>();
        }

		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = dbSet;
			return query.ToList();
		}

		public T GetOne(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			return query.FirstOrDefault();
		}

		//public void Update(T entity)
		//{
		//	dbSet.Update(entity);
		//}
	}
}
