using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        //[HttpGet("search/{userName}")]
        //public async Task<IActionResult> GetUsersByUserName(string userName)
        //{
        //    var users = await _userService.GetUserByPredicateAsync(user => user.UserName == userName);
        //    return Ok(users);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userCreateDto)
        {
            var user = await _userService.CreateUserAsync(userCreateDto);
            return Ok(user);
        }

        //[HttpPut("{userUpdateDto.Id}")]
        //public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        //{
        //    var user = await _userService.UpdateUserAsync(userUpdateDto);
        //    return Ok(user);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var deletedUser = await _userService.DeleteUserByIdAsync(id);
            return Ok(deletedUser);
        }
    }
}
