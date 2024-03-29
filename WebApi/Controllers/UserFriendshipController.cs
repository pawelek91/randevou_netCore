﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;
using BusinessServices.MessageService;
using BusinessServices.FriendshipService;
using WebApi.Controllers.FriendshipsDto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserFriendshipController : BasicBusinessAuthController
    {
        [ProducesResponseType(typeof(int[]),200)]
        [HttpGet("users/{id}/friends")]
        public IActionResult GetUserFriendships(int id)
        {
            if (id == default(int))
                return BadRequest("ID");

            if (id != LoggedUserId)
                return Unauthorized();

            var service = GetService<IFriendshipService>();
            var result = service.GetFriends(id);
            return Ok(result);
        }

        /// <summary>
        /// Status relacji
        /// </summary>
        /// <param name="id">Id zalogowanego</param>
        /// <param name="user2Id">Id zapytanego</param>
        /// <returns>None/Friends/Invited</returns>
        [ProducesResponseType(typeof(string), 200)]
        [HttpGet("users/{id}/RelationStatus/{user2Id}")]
        public IActionResult GetUsersRealtionSattus(int id, int user2Id)
        {
            if (id == default(int))
                return BadRequest("ID");

            if (id != LoggedUserId)
                return Unauthorized();

            var service = GetService<IFriendshipService>();
            var result = service.RelationShipStatus(id, user2Id);
            string strResult = RelationShipStatusConst.None;

            switch(result)
            {
                case RandevouData.Users.RelationStatus.Accepted: strResult = RelationShipStatusConst.Friends;break;
                case RandevouData.Users.RelationStatus.Invited: strResult = RelationShipStatusConst.Invited; break;
                case RandevouData.Users.RelationStatus.Created: strResult = RelationShipStatusConst.Created;break;
            }
            return Ok(strResult);
        }

        [ProducesResponseType(typeof(int[]), 200)]
        [HttpGet("users/{id}/requests")]
        public IActionResult GetUserFriendshipsRequests(int id)
        {
            if (id == default(int))
                return BadRequest("ID");

            if (id != LoggedUserId)
                return Unauthorized();

            var service = GetService<IFriendshipService>();
            var result = service.GetFriendshipRequests(id);
            return Ok(result);
        }

        [ProducesResponseType(typeof(string[]),200)]
        [HttpPost("PossibleRequestsActions")]
        public IActionResult GetPossibleRequestActions()
        {
            return Ok(possibleActions);
        }

        [HttpPut("Invitation")]
        public IActionResult SendFriendshipInvitation([FromBody] FriendshipsDto.FriendshipSendRequestDto dto)
        {
            if (dto == null)
                return BadRequest(nameof(dto));

            if (dto.FromUserId == default(int) || dto.FromUserId != LoggedUserId)
                return BadRequest("FromUserId");

            if (dto.ToUserId == default(int))
                return BadRequest("ToUserId");

            var service = GetService<IFriendshipService>();
            service.SendFriendshipRequest(dto.FromUserId, dto.ToUserId);

            return Ok();
        }

        [HttpPut("FriendshipStatusAction")]
        public IActionResult UpdateFriendshipStatus([FromBody] FriendshipsDto.UpdateFriendshipStatusDto dto)
        {
            if (dto == null)
                return BadRequest(nameof(dto));

            if (dto.FromUserId == default(int) || dto.FromUserId != LoggedUserId)
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
