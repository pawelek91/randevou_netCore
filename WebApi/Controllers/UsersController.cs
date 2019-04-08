using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;
using BusinessServices.MessageService;
using WebApi.Controllers.Auth;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    public class UsersController : BasicController
    {
        [BasicAuth]
        [HttpGet]
        public IActionResult Get()
        {
            IUsersService usersService = GetService<IUsersService>();
            var users = usersService.QueryUsers().ToArray();
            return Ok(users);
        }

        [BasicAuth]
        [HttpGet]
        [Route("{id}/Details")]
        [ProducesResponseType(typeof(UserDetailsDto), 200)]
        public IActionResult GetUserWithDetials(int id)
        {
            IUsersService usersService = GetService<IUsersService>();
            var user = usersService.GetUserWithDetails(id);
            return Ok(user);
        }

        [BasicAuth]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        public IActionResult Get(int id)
        {
            IUsersService usersService = GetService<IUsersService>();
            var user = usersService.GetUser(id);
            if (user == null)
                return NoContent();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult PostNewUser([FromBody]UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(nameof(userDto));

            IUsersService usersService = GetService<IUsersService>();
            var id = usersService.Add(userDto);
            return Created("api/users/", id.ToString());
        }

        [BasicAuth]
        [Route("{id}/Details")]
        [HttpPatch]
        public IActionResult PatchUserDetails([FromHeader]int id, [FromBody]UserDetailsDto detailsDto)
        {
            IUsersService usersService = GetService<IUsersService>();
            if (id == 0 || id != LoggedUserId)
                return BadRequest(nameof(id));

            

            if (detailsDto.UserId == 0)
                detailsDto.UserId = id;

            usersService.UpdateUserDetails(id, detailsDto);
            return Ok();
        }

        [BasicAuth]
        [HttpPatch]
        public IActionResult Patch([FromBody]UserDto userDto)
        {
            if (userDto == null || !userDto.Id.HasValue)
                return BadRequest(nameof(userDto));

            if (userDto.Id != LoggedUserId)
                return Unauthorized();

            IUsersService usersService = GetService<IUsersService>();
            usersService.Update(userDto);
            return Ok();

        }

        [BasicAuth]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IUsersService usersService = GetService<IUsersService>();
            usersService.Delete(id);
            return NoContent();
        }
    }
}
