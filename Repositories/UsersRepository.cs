using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Server.Data;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
	public class UsersRepository(SystemDbContext systemDbContext, IPasswordHasher passwordHasher) : IUsersRepository
	{
		private readonly SystemDbContext _dbContext = systemDbContext;
        private readonly IPasswordHasher passwordHasher = passwordHasher;

		public async Task<UsersModel> FindUserById(int id)
		{
			return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id) 
				?? throw new KeyNotFoundException($"User with Id {id} not found.");
        }

		public async Task<List<UsersModel>> FindAllUsers()
		{
			return await _dbContext.Users.ToListAsync();
		}

		public async Task<UsersModel> FindUserByEmail(string email)
		{

			return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email) 
				?? throw new KeyNotFoundException($"User with email: {email} not found.");	
		}

        public async Task<UsersModel> CreateUser(UsersModel user)
        { 
            var passwordHash = passwordHasher.Hash(user.Password);
            user.Password = passwordHash;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UsersModel> UpdateUser(UsersModel user, int id)
		{
			UsersModel userById = await FindUserById(id) 
				?? throw new Exception($"User by id:{id} not found");

			userById.Name = user.Name;
			userById.Password = user.Password;

			_dbContext.Users.Update(userById);
			await _dbContext.SaveChangesAsync();

			return userById;
		}

        public async Task<bool> DeleteUser(int id)
		{
			UsersModel userById = await FindUserById(id) 
				?? throw new Exception($"User by id:{id} not found");

			_dbContext.Users.Remove(userById);
			await _dbContext.SaveChangesAsync();

			return true;
		}
	}
}