using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CA_Application.Repository
{
	public interface IRepository<T> where T : class
	{
		T GetOne(Expression<Func<T, bool>> filter);
		IEnumerable<T> GetAll();
		void Add(T entity);
		//void Update(T entity);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);

	}
}
