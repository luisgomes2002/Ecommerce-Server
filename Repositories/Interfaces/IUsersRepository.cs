using Server.Models;

namespace Server.Repositories.Interfaces
{
	public interface IUsersRepository
	{
		Task<List<UsersModel>> FindAllUsers(int userId);
		Task<UsersModel> FindUserById(int id);
        Task<UsersModel> FindUserByEmail(string email);
        Task<UsersModel> CreateUser(UsersModel user);
		Task<UsersModel> UpdateUser(UsersModel user, int id);
		Task<bool> DeleteUser(int id);
	}
}