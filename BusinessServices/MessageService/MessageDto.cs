using System;

namespace BusinessServices.MessageService
{

    public class MessageBasicDto
    {
        public int MessageId{get;set;}
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
    }
    public class MessageDto : MessageBasicDto
    {
        public string SenderName{get;set;}        
        public string ReceiverName{get;set;}
        public DateTime SendDate{get;set;}
        public bool IsRead{get;set;}
    }
}