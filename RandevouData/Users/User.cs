using RandevouData.Messages;
using RandevouData.Users.Details;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RandevouData.Users
{
    public class User : BasicRandevouObject
    {
        [Required]
        public string Name{get;set;}

        public string DisplayName{get;set;}

        [Required]
        public char Gender{get;set;}

        [Required]
        public DateTime BirthDate{get;set;}

        public User(string n, string dn, char g, DateTime bd)
        {
            Name=n; DisplayName = dn; Gender = g; BirthDate = bd;
            UserDetails = new UserDetails(this);
        }

        public virtual UserDetails UserDetails { get; set; }

        protected User(){}

        public override string ToString()
        {
            return string.Format("{0} ({1}) {2}", this.Name, this.Gender,this.BirthDate.ToShortDateString());
        }
    }
}