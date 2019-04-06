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
    public class LoginController : BasicController
    {
        [HttpPost("Login")]
        public HttpResponseMessage PostLogin(string username, string password)
        {
            var loginService = GetService<IAuthenticationService>();
            var userService = GetService<IUsersService>();

            var userId = userService.QueryUsers().Where(x => x.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()?.Id;
            if (userId == null)
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            var key = loginService.LoginUser(userId.Value, password);
            if(string.IsNullOrEmpty(key))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) { Content = new StringContent(key) };
        }

        [HttpPost("Register")]
        public IActionResult RegisterLogin(int userId, string password)
        {
            var loginService = GetService<IAuthenticationService>();
            loginService.RegisterUser(userId, password);
            return Ok();
        }
    }
}
