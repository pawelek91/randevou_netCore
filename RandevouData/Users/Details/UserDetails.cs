using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouData.Users.Details
{
    public class UserDetails
    {
        protected UserDetails()
        {

        }
        public UserDetails(User user)
        {
            this.UserId = user.Id;
            this.User = user;
        }
        public int Id { get; set; }
        public int Width { get; set; }
        public int Heigth { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int Tattos { get; set; }
        //public IList<UsersDetailsDictionaryItems> DetailsDictionaryItems { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public ICollection<UsersDetailsItemsValues> DetailsItemsValues{get;set;}
    }
}
