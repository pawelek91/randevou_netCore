using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RandevouData.Users
{
    public class UsersFriendship :  BasicRandevouObject
    {

        public User User1 { get; set; }
        public User User2 { get; set; }

        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public RelationStatus RelationStatus { get; set; }

        protected UsersFriendship() { }

        public UsersFriendship(User user1, User user2)
        {
            if (user1 == null || user2==null)
                throw new ArgumentException("User not exists");

            if (user1.Id == user2.Id)
                throw new ArgumentException("Wrog set of data");

            this.User1 = user1;
            this.User2 = user2;
            this.User1Id = user1.Id;
            this.User2Id = user2.Id;

            RelationStatus = RelationStatus.Invited;
        }
    }

    public enum RelationStatus
    {
        Created,
        Invited,
        Deleted,
        Accepted,
    }
}
