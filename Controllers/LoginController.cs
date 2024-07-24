using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginRepository iLoginRepository, TokenRepository tokenRepository) : ControllerBase
    {
        private readonly ILoginRepository iLoginRepository = iLoginRepository;
        private readonly TokenRepository tokenRepository = tokenRepository;

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