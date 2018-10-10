using System;
using System.ComponentModel.DataAnnotations;
using RandevouData.Users;

namespace RandevouData.Messages
{
    public class Message : BasicRandevouObject
    {
        [Required]
        public Users.User FromUser{get;set;}
        
        [Required]
        public Users.User ToUser{get;set;}

        [Required]
        public DateTime SendDate{get;set;}
        public DateTime? ReadDate{get;set;}

        [Required]
        public string MessageContent{get;set;}

        public Message(){}

        public Message(User from, User to, string content)
        {
            this.FromUser = from;
            this.ToUser = to;
            this.MessageContent = content;
            this.SendDate = DateTime.Now;
            this.ReadDate = DateTime.Now;
        }

    }
}