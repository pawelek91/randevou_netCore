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
    public class MessagesController : BasicController
    {
        [Route(ApiConsts.Conversation)]
        [HttpPost]
        public IActionResult GetConversation([FromBody] RequestMessagesDto dto)
        {
            IMessagesService messagesService = GetService<IMessagesService>();
            var result = messagesService.GetConversationBetweenUsers(dto.FirstUserId, dto.SecondUserId);
            return Ok(result);
        }

        
        [Route("{userId:int}/"+ApiConsts.Speakers)]
        [HttpGet]
        public IActionResult GetSpeakers(int userId)
        {
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

        [HttpPost]
        public IActionResult PostMessage([FromBody] MessageBasicDto dto)
        {
            IMessagesService messagesService = GetService<IMessagesService>();
            var messageId = messagesService.SendMessage(dto.SenderId, dto.ReceiverId, dto.Content);
            return Created("api/messages/",messageId);
        }

        [Route("{userId:int}/"+ApiConsts.Conversation)]
        [HttpGet]
        public IActionResult GetConvesationsLastMessages(int userId)
        {
            IMessagesService messagesService = GetService<IMessagesService>();
            var result = messagesService.GetConversationsLastMessages(userId);
            return Ok(result);
        }
    }
}
