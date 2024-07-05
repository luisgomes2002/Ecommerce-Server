using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]

	public class UsersController : Controller
	{
		private readonly IUsersRepository iUserRepository;
        private readonly TokenRepository tokenRepository;

        public UsersController(IUsersRepository iUserRepository, TokenRepository tokenRepository)
        {
            this.iUserRepository = iUserRepository;
            this.tokenRepository = tokenRepository;
        }

        [NonAction]
        public async Task<int> GetUserIdByToken()
        {
            var authHeader = Request.Headers.Authorization.ToString();
            var token = authHeader.Replace("Bearer ", "");

            int userId = await tokenRepository.VerifyToken(token);
            return userId;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UsersModel>>> FindAllUser()
		{
            try
            {
                int userId = await GetUserIdByToken();
                List<UsersModel> users = await iUserRepository.FindAllUsers(userId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao mostrar Todos os Usuarios", ex);
            }
		}

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<UsersModel>>> FindUserById(int id)
        {
            try
            {
                int userId = await GetUserIdByToken();
                UsersModel user = await iUserRepository.FindUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao mostrar usuario by Id", ex);
            }  
        }

		[HttpPost]
		public async Task<ActionResult<UsersModel>> CreateUser([FromBody] UsersModel userModel)
		{
            try
            {
                UsersModel user = await iUserRepository.CreateUser(userModel);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuario", ex);
            }
			
		}

        [HttpPut("{id}")]
        public async Task<ActionResult<UsersModel>> UpdateUser([FromBody] UsersModel userModel, int id)
        {
            try
            {
                userModel.Id = id;
                UsersModel user = await iUserRepository.UpdateUser(userModel, id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o usuario", ex);
            }
           
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<UsersModel>> DeleteUser(int id)
		{
            try
            {
                bool excluded = await iUserRepository.DeleteUser(id);
                return Ok(excluded);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar o usuario", ex);
            }
            
		}
    } 
}