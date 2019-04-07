using BusinessServices.MessageService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers.RequestDto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : BasicBusinessController
    {
        [ProducesResponseType(typeof(List<MessageDto>),200)]
        [HttpPost(ApiConsts.Conversation)]
        public IActionResult GetConversation([FromBody] RequestMessagesDto dto)
        {
            dto.FirstUserId = LoggedUserId.Value;

            IMessagesService messagesService = GetService<IMessagesService>();
            var result = messagesService.GetConversationBetweenUsers(dto.FirstUserId, dto.SecondUserId);
            return Ok(result);
        }

        [ProducesResponseType(typeof(int[]), 200)]
        [Route("{userId}/"+ApiConsts.Speakers)]
        [HttpGet]
        public IActionResult GetSpeakers(int userId)
        {
            if (userId != LoggedUserId)
                return Unauthorized();

            IMessagesService messagesService = GetService<IMessagesService>();
            var result = messagesService.GetUserConversationsSpeakers(userId);
            return Ok(result);
        }

        //[HttpGet]
        //public IActionResult GetSingleMessage([FromHeader] int messageId)
        //{
        //    IMessagesService messagesService = GetService<IMessagesService>();
        //    var result = messagesService.GetMessage(messageId);
        //    return Ok(result);
        //}


        [ProducesResponseType(typeof(int),201)]
        [HttpPost]
        public IActionResult PostMessage([FromBody] MessageBasicDto dto)
        {
            if (dto.SenderId != LoggedUserId)
                return Unauthorized();

            IMessagesService messagesService = GetService<IMessagesService>();
            var messageId = messagesService.SendMessage(dto.SenderId, dto.ReceiverId, dto.Content);
            return Created("api/messages/",messageId);
        }

         [ProducesResponseType(typeof(List<LastMessagesFromConversationsDto>),200)]
         [HttpGet(  ApiConsts.Conversation + "/{userId}")]
         public IActionResult GetConvesationsLastMessages(int userId)
         {
            if (userId != LoggedUserId)
                return Unauthorized();

            IMessagesService messagesService = GetService<IMessagesService>();
             var result = messagesService.GetConversationsLastMessages(userId);
             return Ok(result);
         }

        [HttpPut(ApiConsts.MarkRead)]
        public IActionResult MarkAsRead([FromBody] MessageMarkDto dto)
        {
            if (dto.OwnerId != LoggedUserId)
                return Unauthorized();

            IMessagesService messagesService = GetService<IMessagesService>();
            if (dto == null || dto.OwnerId == default(int) || dto.MessageId == default(int))
                return BadRequest("Wrong dto");

            messagesService.MarkMessageRead(dto.MessageId, dto.OwnerId);
            return Ok();
        }

        [HttpPut(ApiConsts.MarkUnread)]
        public IActionResult MarkAsUnread([FromBody] MessageMarkDto dto)
        {
            if (dto.OwnerId != LoggedUserId)
                return Unauthorized();

            IMessagesService messagesService = GetService<IMessagesService>();
            if (dto == null || dto.OwnerId == default(int) || dto.MessageId == default(int))
                return BadRequest("Wrong dto");

            messagesService.MarkMessageUnread(dto.MessageId, dto.OwnerId);
            return Ok();
        }
    }
}
