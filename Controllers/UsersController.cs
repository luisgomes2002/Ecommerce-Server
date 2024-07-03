using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]

	public class UsersController : Controller
	{
		private readonly IUsersRepository iUserRepository;	

		public UsersController(IUsersRepository iUserRepository)
        {
            this.iUserRepository = iUserRepository;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UsersModel>>> FindAllUser()
		{
           List<UsersModel> users = await iUserRepository.FindAllUsers();
           return Ok(users);
		}

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<UsersModel>>> FindUserById(int id)
        {
            UsersModel user = await iUserRepository.FindUserById(id);
            return Ok(user);
        }

		[HttpPost]
		public async Task<ActionResult<UsersModel>> CreateUser([FromBody] UsersModel userModel)
		{
			UsersModel user = await iUserRepository.CreateUser(userModel);
			return Ok(user);
		}

        [HttpPut("{id}")]
        public async Task<ActionResult<UsersModel>> UpdateUser([FromBody] UsersModel userModel, int id)
        {
			userModel.Id = id;
            UsersModel user = await iUserRepository.UpdateUser(userModel, id);
            return Ok(user);
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<UsersModel>> DeleteUser(int id)
		{
			bool excluded = await iUserRepository.DeleteUser(id);
			return Ok(excluded);
		}
    } 
}