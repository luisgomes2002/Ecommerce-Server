using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly SystemDbContext _dbContext;

		public UserRepository(SystemDbContext systemDbContext)
		{
			_dbContext = systemDbContext;
		}

		public async Task<UserModel> FindUserById(int id)
		{
			return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<UserModel>> FindAllUsers()
		{
			return await _dbContext.Users.ToListAsync();
		}

		public async Task<UserModel> CreateUser(UserModel user)
		{
			await _dbContext.Users.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			return user;
		}

		public async Task<UserModel> UpdateUser(UserModel user, int id)
		{
			UserModel userById = await FindUserById(id);

			if (userById == null) throw new Exception($"User by id:{id} not found");

			userById.Name = user.Name;
			userById.Password = user.Password;

			_dbContext.Users.Update(userById);
			await _dbContext.SaveChangesAsync();

			return userById;
		}

		public async Task<bool> DeleteUser(int id)
		{
			UserModel userById = await FindUserById(id);

			if (userById == null) throw new Exception($"User by id:{id} not found");

			_dbContext.Users.Remove(userById);
			await _dbContext.SaveChangesAsync();

			return true;
		}
	}
}