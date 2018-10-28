using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RandevouData.Users;

namespace RandevouData.Messages
{
   
    public class Message : BasicRandevouObject
    {
        [NotMapped]
        [Required]
        public virtual User FromUser{get;set;}

        [NotMapped]
        [Required]
        public virtual User ToUser{get;set;}

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