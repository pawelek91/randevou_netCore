using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouData.Users.Details
{
    public class UserDetailsDictionaryItem:BasicRandevouObject
    {
        public string DetailsType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ObjectType { get; set; }
        //IList<UsersDetailsDictionaryItems> DetailsDictionaryItems { get; set; }
    }
}
