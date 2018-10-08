using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
	public class UsersController : BasicController
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
			IUsersService usersService = GetService<IUsersService>();
			var users = usersService.QueryUsers().ToArray();
			return users.Any() ? users.Select(x=>x.ToString()) : new string[1]{"lista userow jest pusta"} ;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
			IUsersService usersService = GetService<IUsersService>();
			var user = usersService.GetUser(id);
			return user != null ? user.Name : "Brak usera";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]UserDto userDto)
        {
            DateTime birthDate;
            var name = userDto.UserName;
            DateTime.TryParse(userDto.BirthDate, out birthDate);
            var gender = userDto.Gender;
            IUsersService usersService = GetService<IUsersService>();
            usersService.Add(name,gender,birthDate);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
