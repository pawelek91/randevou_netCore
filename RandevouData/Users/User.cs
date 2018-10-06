using System;

namespace RandevouData.Users
{
    public class User : BasicRandevouObject
    {
        public string Name{get;set;}
        public string DisplayName{get;set;}

        public char Gender{get;set;}

        public DateTime BirthDate{get;set;}

        public User(string n, string dn, char g, DateTime bd)
        {
            Name=n; DisplayName = dn; Gender = g; BirthDate = bd;
        }
    }
}