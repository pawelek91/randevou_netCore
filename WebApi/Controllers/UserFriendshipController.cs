using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;
using BusinessServices.MessageService;
using BusinessServices.FriendshipService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserFriendshipController : BasicController
    {
        [ProducesResponseType(typeof(int[]),200)]
        [HttpGet("users/{id}/friends")]
        public IActionResult GetUserFriendships(int id)
        {
            if (id == default(int))
                return BadRequest("ID");

            var service = GetService<IFriendshipService>();
            var result = service.GetFriends(id);
            return Ok(result);
        }

        [ProducesResponseType(typeof(int[]), 200)]
        [HttpGet("users/{id}/requests")]
        public IActionResult GetUserFriendshipsRequests(int id)
        {
            if (id == default(int))
                return BadRequest("ID");

            var service = GetService<IFriendshipService>();
            var result = service.GetFriendshipRequests(id);
            return Ok(result);
        }

        [HttpPost("PossibleRequestsActions")]
        public IActionResult GetPossibleRequestActions()
        {
            return Ok(possibleActions);
        }

        [HttpPost("/Invitation")]
        public IActionResult SendFriendshipInvitation([FromBody] FriendshipsDto.FriendshipSendRequestDto dto)
        {
            if (dto == null)
                return BadRequest(nameof(dto));

            if (dto.FromUserId == default(int))
                return BadRequest("FromUserId");

            if (dto.ToUserId == default(int))
                return BadRequest("ToUserId");

            var service = GetService<IFriendshipService>();
            service.SendFriendshipRequest(dto.FromUserId, dto.ToUserId);

            return Ok();
        }
        [HttpPut("/FriendshipStatusAction")]
        public IActionResult UpdateFriendshipStatus([FromBody] FriendshipsDto.UpdateFriendshipStatusDto dto)
        {
            if (dto == null)
                return BadRequest(nameof(dto));

            if (dto.FromUserId == default(int))
                return BadRequest("FromUserId");

            if (dto.ToUserId == default(int))
                return BadRequest("ToUserId");

            if (string.IsNullOrEmpty(dto.Action) || !possibleActions.Any(x => x == dto.Action))
                return BadRequest("Action");

            var service = GetService<IFriendshipService>();
            service.UpdateFriendshipStatus(dto.FromUserId, dto.ToUserId, dto.Action);

            return Ok();
        }

        private string[] possibleActions
            => new[] { FriendshipsConsts.Accept, FriendshipsConsts.Delete };
    }
}
