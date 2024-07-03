using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IPasswordHasher passwordHasher;
        private readonly IUsersRepository iUsersRepository;

        public LoginRepository(IPasswordHasher passwordHasher, IUsersRepository iUsersRepository)
        {
            this.passwordHasher = passwordHasher;
            this.iUsersRepository = iUsersRepository;   
        }

        public async Task<LoginModel> Login(LoginModel userLogin)
        {
           
            UsersModel user = await iUsersRepository.FindUserByEmail(userLogin.Email)
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
