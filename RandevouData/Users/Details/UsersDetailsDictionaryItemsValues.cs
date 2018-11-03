using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RandevouData.Users.Details
{
    public class UsersDetailsItemsValues
    {

        public UsersDetailsItemsValues() { }
        public UsersDetailsItemsValues(UserDetails ud)
        {
            this.UserDetailsId = ud.Id;
        }

        [Key]
        public int UserDetailsId {get;set;}
        [Key]
        public int UserDetailsDictionaryItemId{get;set;} 
        
        public bool Value{get;set;}
    }
}
