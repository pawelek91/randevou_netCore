using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.MessageService
{
    /// <summary>
    /// This Dto will represent list of all conversations 
    /// displaying only last message of conversation
    /// </summary>
    public class LastMessagesFromConversationsDto
    {
        public int MessageId{get;set;}
        public int SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        public bool IsRead { get; set; }
        public string MessageShortContent { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
