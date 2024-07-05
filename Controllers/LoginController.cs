using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using Server.Repositories.Interfaces;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository iLoginRepository;
        private readonly TokenRepository tokenRepository;

        public LoginController(ILoginRepository iLoginRepository, TokenRepository tokenRepository)
        {
            this.iLoginRepository = iLoginRepository;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel login)
        {
            try
            {
                LoginModel userLogin = await iLoginRepository.Login(login);
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