using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RandevouData.Users.Details
{
    public class UserDetailsDictionaryItem:BasicRandevouObject
    {
        [Required]
        public string DetailsType { get; set; }

        [Required]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        [Required]
        public string ObjectType { get; set; }

        //public ICollection<UsersDetailsItemsValues> DetailsItemsValues{get;set;}
    }
}
