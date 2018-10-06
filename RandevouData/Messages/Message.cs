using System;

namespace RandevouData.Messages
{
    public class Message : BasicRandevouObject
    {
        public Users.User FromUser{get;set;}
        public Users.User ToUser{get;set;}

        public DateTime SendDate{get;set;}
        public DateTime ReadDate{get;set;}

    }
}