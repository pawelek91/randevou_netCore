using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessServices.AuthenticationService;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    public class LoginController : BasicController
    {
        [HttpPost("Login")]
        public IActionResult PostLogin([FromBody] LoginDto dto)
        {
            var loginService = GetService<IAuthenticationService>();
            var userService = GetService<IUsersService>();
            var key = loginService.LoginUser(dto.UserName, dto.Password);
            if(string.IsNullOrEmpty(key))
                return Unauthorized();

            return Ok(key);

        }

        [HttpPost("Register")]
        public IActionResult RegisterLogin([FromBody] RegisterDto dto)
        {
            var loginService = GetService<IAuthenticationService>();
            loginService.RegisterUser(dto.UserId, dto.Password);
            return Ok();
        }
    }
}
