using Server.Models;

namespace Server.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<LoginModel> Login(LoginModel login);
    }
}
