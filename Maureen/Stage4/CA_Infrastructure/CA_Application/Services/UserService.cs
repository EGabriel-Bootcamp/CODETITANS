using CA_Application.Repository;
using CA_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CA_Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		public UserService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Add(User entity)
		{
			_unitOfWork.UserRepo.Add(entity);
		}

		public void Delete(User entity)
		{
			var user = _unitOfWork.UserRepo.GetOne(x => x.Id == entity.Id);
			_unitOfWork.UserRepo.Delete(user);
		}

		public void DeleteRange(IEnumerable<User> entities)
		{
			_unitOfWork.UserRepo.DeleteRange(entities);
		}

		public IEnumerable<User> GetAll()
		{
			return _unitOfWork.UserRepo.GetAll();
		}

		public User GetOne(Expression<Func<User, bool>> filter)
		{
			var user = _unitOfWork.UserRepo.GetOne(filter);
			return user;
		}

		public void Update(User entity)
		{
			//var entityFromDb = _unitOfWork.UserRepo.GetOne(x => x.Id == entity.Id);

			//entityFromDb.Address = entity.Address;
			//entityFromDb.City = entity.City;
			//entityFromDb.MarritalStatus = entity.MarritalStatus;
			
			_unitOfWork.UserRepo.Update(entity);
		}

		public void Save()
		{
			_unitOfWork.Save();
		}

	}
}
