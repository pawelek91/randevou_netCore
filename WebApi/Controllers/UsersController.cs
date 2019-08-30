using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;
using BusinessServices.MessageService;
using WebApi.Controllers.Auth;
using Microsoft.AspNetCore.Http;
using System.IO;
using WebApi.Common;

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
        [HttpPost]
        [Route("List")]
        public IActionResult GetMany([FromBody] int[] ids)
        {
            if(ids == null)
            {
                return BadRequest();
            }
            IUsersService usersService = GetService<IUsersService>();
            var users = usersService.QueryUsers(ids).ToArray();
            return Ok(users);
        }

        [BasicAuth]
        [HttpPost]
        [Route("List/Avatars")]
        public IActionResult GetAvatars([FromBody] int[] ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }
            IUsersService usersService = GetService<IUsersService>();
            var usersAvatars = usersService.GetUsersAvatars(ids)
                .Where(x => x.AvatarContentBytes!= null && x.AvatarContentBytes.Length > 0 && x.AvatarContentType?.Length > 0);
            
            return Ok(usersAvatars);
        }

        [BasicAuth]
        [HttpPost]
        [Route("List/Avatars/base64img")]
        public IActionResult GetAvatarsBase64([FromBody] int[] ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }
            IUsersService usersService = GetService<IUsersService>();
            var usersAvatars = usersService.GetUsersAvatars(ids)
                .Where(x => x.AvatarContentBytes != null && x.AvatarContentBytes.Length > 0 && x.AvatarContentType?.Length > 0)
                .Select(x => new AvatarDto
                {
                    UserId = x.UserId,
                    ContentType = x.AvatarContentType,
                    Base64Content = Convert.ToBase64String(x.AvatarContentBytes, 0, x.AvatarContentBytes.Length),
                });
            return Ok(usersAvatars);
        }

        //[BasicAuth]
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

        /// <summary>
        /// Set user avatar
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="file">image</param>
        /// <returns></returns>
        [HttpPut("{id}/details/avatar")]
        public async Task<IActionResult> UploadAvatar(int id, [FromForm]IFormFile file)
        {

            if (id != LoggedUserId)
                return Unauthorized();

            if (file == null || file.Length == 0 || !file.IsImage())
                return BadRequest("file is empty or wrong file content");


            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var fileData = stream.ToArray();
                IUsersService usersService = GetService<IUsersService>();
                usersService.SetAvatar(id, stream, file.ContentType);
            }
            return Ok();
            
        }

        /// <summary>
        /// Set user avatar
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="file">image</param>
        /// <returns></returns>
        [HttpPut("{id}/details/avatar/base64")]
        public IActionResult UploadAvatar(int id, [FromBody] AvatarDto dto)
        {
            if (id != LoggedUserId)
                return Unauthorized();

            if(dto == null || string.IsNullOrWhiteSpace(dto.Base64Content) || string.IsNullOrWhiteSpace(dto.ContentType))
                return BadRequest();

            IUsersService usersService = GetService<IUsersService>();
            usersService.SetAvatar(id, dto.Base64Content, dto.ContentType);
            
            return Ok();
        }

    }
}
