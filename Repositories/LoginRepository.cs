using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
    public class LoginRepository(IPasswordHasher passwordHasher, IUsersRepository usersRepository) : ILoginRepository
    {
        private readonly IPasswordHasher passwordHasher = passwordHasher;
        private readonly IUsersRepository usersRepository = usersRepository;

        public async Task<LoginModel> Login(LoginModel userLogin)
        {
           
            UsersModel user = await usersRepository.FindUserByEmail(userLogin.Email)
                ?? throw new Exception($"User by email:{userLogin.Email} not found");

            var passwordVerify = passwordHasher.Verify(user.Password, userLogin.Password);

            if (!passwordVerify)
            {
                throw new Exception("Username or password is not correct");
            }

            return userLogin;
        }
    }
}
