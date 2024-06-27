using Server.Models;

namespace Server.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<List<UserModel>> FindAllUsers();
		Task<UserModel> FindUserById(int id);
		Task<UserModel> CreateUser(UserModel user);
		Task<UserModel> UpdateUser(UserModel user, int id);
		Task<bool> DeleteUser(int id);
	}
}