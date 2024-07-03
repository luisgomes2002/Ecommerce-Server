using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]

	public class UsersController(IUsersRepository userRepository) : Controller
	{
		private readonly IUsersRepository userRepository = userRepository;	

		[HttpGet]
        [Authorize]
        public async Task<ActionResult<List<UsersModel>>> FindAllUser()
		{
           List<UsersModel> users = await userRepository.FindAllUsers();
           return Ok(users);
		}

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<UsersModel>>> FindUserById(int id)
        {
            UsersModel user = await userRepository.FindUserById(id);
            return Ok(user);
        }

		[HttpPost]
		public async Task<ActionResult<UsersModel>> CreateUser([FromBody] UsersModel userModel)
		{
			UsersModel user = await userRepository.CreateUser(userModel);
			return Ok(user);
		}

        [HttpPut("{id}")]
        public async Task<ActionResult<UsersModel>> UpdateUser([FromBody] UsersModel userModel, int id)
        {
			userModel.Id = id;
            UsersModel user = await userRepository.UpdateUser(userModel, id);
            return Ok(user);
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<UsersModel>> DeleteUser(int id)
		{
			bool excluded = await userRepository.DeleteUser(id);
			return Ok(excluded);
		}
    } 
}