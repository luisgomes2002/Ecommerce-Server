using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class UserController : Controller
	{
		private readonly IUserRepository userRepository;
		public UserController(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}	

		[HttpGet]
		public async Task<ActionResult<List<UserModel>>> FindAllUser()
		{
           List<UserModel> users = await userRepository.FindAllUsers();
           return Ok(users);
		}

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserModel>>> FindUserById(int id)
        {
            UserModel user = await userRepository.FindUserById(id);
            return Ok(user);
        }

		[HttpPost]
		public async Task<ActionResult<UserModel>> CreateUser([FromBody] UserModel userModel)
		{
			UserModel user = await userRepository.CreateUser(userModel);
			return Ok(user);
		}

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel userModel, int id)
        {
			userModel.Id = id;
            UserModel user = await userRepository.UpdateUser(userModel, id);
            return Ok(user);
        }

		[HttpDelete("{id}")]
		public async Task<ActionResult<UserModel>> DeleteUser(int id)
		{
			bool excluded = await userRepository.DeleteUser(id);
			return Ok(excluded);
		}
    } 
}