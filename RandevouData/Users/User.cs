using System;
using System.ComponentModel.DataAnnotations;

namespace RandevouData.Users
{
    public class User : BasicRandevouObject
    {
        [Required]
        public string Name{get;set;}
        public string DisplayName{get;set;}

        public char Gender{get;set;}

        public DateTime BirthDate{get;set;}

        public User(string n, string dn, char g, DateTime bd)
        {
            Name=n; DisplayName = dn; Gender = g; BirthDate = bd;
        }

        protected User(){}

        public override string ToString()
        {
            return string.Format("{0} ({1}) {2}", this.Name, this.Gender,this.BirthDate.ToShortDateString());
        }
    }
}