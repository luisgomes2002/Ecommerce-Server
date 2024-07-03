using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using Server.Repositories.Interfaces;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginRepository loginRepository, TokenRepository tokenRepository, IUsersRepository usersRepository) : ControllerBase
    {
        private readonly ILoginRepository loginRepository = loginRepository;
        private readonly TokenRepository tokenRepository = tokenRepository;
        private readonly IUsersRepository usersRepository = usersRepository;

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel login)
        {
            try
            {
                LoginModel userLogin = await loginRepository.Login(login);

                var token = tokenRepository.GenerateTokenJwt(login.Email);
                
                return token;
            }
            catch (Exception ex)
            {            
                return BadRequest(ex.Message);                 
            }
        }
    }
}