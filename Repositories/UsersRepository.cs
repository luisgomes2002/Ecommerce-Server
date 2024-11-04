using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.DataBaseTables;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
    public class UsersRepository(UsersDbConnections usersDbConnections, IPasswordHasher iPasswordHasher) : IUsersRepository
    {
        private readonly UsersDbConnections usersDbConnections = usersDbConnections;
        private readonly IPasswordHasher iPasswordHasher = iPasswordHasher;

        public async Task<UsersModel> FindUserById(int userId)
        {
            return await usersDbConnections.FindUserByIdDb(userId)
                ?? throw new KeyNotFoundException($"User with Id {userId} not found.");
        }

        public async Task<List<UsersModel>> FindAllUsers(int userId)
        {
            UsersModel userInfo = await FindUserById(userId);

            if (!userInfo.IsMod) throw new("This user does not have permission to see users");

            return await usersDbConnections.FindAllUsersDb()
                ?? throw new KeyNotFoundException($"Users not found.");
        }

        public async Task<UsersModel> FindUserByEmail(string userEmail)
        {

            return await usersDbConnections.FindUserByEmailDb(userEmail)
                ?? throw new KeyNotFoundException($"User with email: {userEmail} not found.");
        }

        public async Task<UsersModel> CreateUser(UsersModel user)
        {
            var passwordHash = iPasswordHasher.Hash(user.Password);
            user.Password = passwordHash;

            await usersDbConnections.CreateUserDb(user);

            return user;
        }

        public async Task<UsersModel> UpdateUser(UsersModel user, int userId)
        {
            UsersModel userById = await FindUserById(userId)
                ?? throw new Exception($"User by id:{userId} not found");
            
            userById.Name = user.Name;
            userById.Password = user.Password;
            userById.Email = user.Email;


            bool result = await usersDbConnections.UpdateUserDb(1, userById);
            if (result) Console.WriteLine("User updated successfully.");
            else Console.WriteLine("Failed to update user.");

            return userById;
        }

        public async Task<bool> DeleteUser(int userid)
        {
            UsersModel userById = await FindUserById(userid)
                ?? throw new Exception($"User by id:{userid} not found");

            bool deleteResponse = await usersDbConnections.DeleteUserDb(userid);

            return deleteResponse;
        }
    }
}