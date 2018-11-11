using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Users;

namespace EFRandevouDAL.Users
{
    public class FriendshipDao : BasicDao<UsersFriendship>
    {
        public FriendshipDao(RandevouDbContext dbc) : base(dbc) { }

        public override void Delete(UsersFriendship entity)
        {
            entity.RelationStatus = RelationStatus.Deleted;
            dbc.SaveChanges();
        }

        public IQueryable<UsersFriendship> QueryFriendships()
        {
            return dbc.Friendships.AsQueryable();
        }
    }
}
