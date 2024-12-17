using HotelManagement.Core.Dtos.Auth;
using HotelManagement.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
          var userDto=await _userService.LoginAsync(loginDto);
            if(userDto is null)
               return Unauthorized();
            
            return Ok(userDto);


        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto=await _userService.RegisterAsync(registerDto);
            if (userDto is null)
                return BadRequest();

            return Ok(userDto);

        }

    }
}
