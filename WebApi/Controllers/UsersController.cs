﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;
using BusinessServices.MessageService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BasicController
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            IUsersService usersService = GetService<IUsersService>();
            var users = usersService.QueryUsers().ToArray();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IUsersService usersService = GetService<IUsersService>();
            var user = usersService.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserDto userDto)
        {
            if (userDto == null)
                return BadRequest(nameof(userDto));

            IUsersService usersService = GetService<IUsersService>();
            var id = usersService.Add(userDto);
            return Created("api/users/", id.ToString());
        }

        [Route("{id:int}/Details")]
        [HttpPost("{id:int}")]
        public IActionResult Post([FromQuery] int id, [FromBody]UserDetailsDto detailsDto)
        {
            IUsersService usersService = GetService<IUsersService>();
            if (id == 0)
                id = detailsDto.UserId;

            usersService.UpdateUserDetails(id, detailsDto);
            return Ok();
        }

        [HttpPatch]
        public IActionResult Patch([FromBody]UserDto userDto)
        {
            if (userDto == null || !userDto.Id.HasValue)
                return BadRequest(nameof(userDto));

            IUsersService usersService = GetService<IUsersService>();
            usersService.Update(userDto);
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IUsersService usersService = GetService<IUsersService>();
            usersService.Delete(id);
            return NoContent();
        }
    }
}
