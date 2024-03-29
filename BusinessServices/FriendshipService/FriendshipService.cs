﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFRandevouDAL.Users;
using RandevouData.Users;

namespace BusinessServices.FriendshipService
{
    public class FriendshipService : IFriendshipService
    {
        public int[] GetFriends(int userId)
        {
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var dao = new FriendshipDao(dbc);

                var query1Ids = dao.QueryFriendships()
                    .Where(x => x.User1Id == userId
                    && x.RelationStatus == RelationStatus.Accepted)
                    .Select(x => x.User2Id)?.ToArray();

                var query2Ids = dao.QueryFriendships()
                    .Where(x => x.User2Id == userId
                    && x.RelationStatus == RelationStatus.Accepted)
                    .Select(x => x.User1Id)?.ToArray();

                int[] firstResult = query1Ids ?? new int[0];
                var friendsIds = firstResult.Concat(query2Ids ?? new int[0]).Distinct();

                return friendsIds.ToArray();
            }
        }

        public int[] GetFriendshipRequests(int userId)
        {
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            { 
                var dao = new FriendshipDao(dbc);
                var usersOfQueriesIds = dao.QueryFriendships().Where(x => x.User1Id == userId && x.RelationStatus == RandevouData.Users.RelationStatus.Invited)
                    .Select(x => x.User2Id)?.ToArray();

                return usersOfQueriesIds;
            }
        }

        public void SendFriendshipRequest(int fromUserId, int toUserId)
        {
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var friendshipDao = new FriendshipDao(dbc);

                var fromUser = usersDao.Get(fromUserId);
                if (fromUser == null)
                    throw new ArgumentException(nameof(fromUserId));


                var toUser = usersDao.Get(toUserId);
                if (toUser == null)
                    throw new ArgumentException(nameof(toUser));

                var relationBetweenUsers = friendshipDao.QueryFriendships()
                    .Where(x => x.User1Id == fromUserId && x.User2Id == toUserId || x.User2Id == fromUserId && x.User1Id == toUserId);
                  

                if (relationBetweenUsers.Any())
                {

                    if(relationBetweenUsers.Any(x=>x.RelationStatus != RelationStatus.Deleted))
                        throw new ArgumentException("ivntitation to friendship has been already sent by one of the user");

                    var firstRelation = relationBetweenUsers.Where(x => x.User1Id == fromUserId).Single();
                    firstRelation.RelationStatus = RelationStatus.Created;

                    var secondRelation = relationBetweenUsers.Where(x => x.User1Id == toUserId).Single();
                    secondRelation.RelationStatus = RelationStatus.Invited;

                    friendshipDao.Update(firstRelation);
                    friendshipDao.Update(secondRelation);

                }

                //if (RelationExists(friendshipDao, fromUserId, toUserId))
                //    throw new ArgumentException("relation between users already exists");
                else
                { 
                    var request = new UsersFriendship(fromUser, toUser, RelationStatus.Created);
                    var request2 = new UsersFriendship(toUser, fromUser, RelationStatus.Invited);

                    friendshipDao.Insert(request);
                    friendshipDao.Insert(request2);
                }         
            }
        }


        public void UpdateFriendshipStatus(int fromUserId, int toUserId, string action)
        {
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var dao = new FriendshipDao(dbc);
                if (!RelationExists(dao, fromUserId, toUserId))
                    throw new ArgumentOutOfRangeException(nameof(action));

                if (action.ToLower() == FriendshipsConsts.Accept.ToLower())
                {
                    var relation = dao.QueryFriendships()
                        .Where(x => x.User2Id == fromUserId && x.User1Id == toUserId && x.RelationStatus == RelationStatus.Created)
                        .FirstOrDefault();

                    if (relation == null)
                        throw new ArgumentException("Relation not exists");

                    relation.RelationStatus = RelationStatus.Accepted;
                    dao.Update(relation);

                    var secondRelation = dao.QueryFriendships()
                        .Where(x => x.User1Id == fromUserId && x.User2Id == toUserId && x.RelationStatus == RelationStatus.Invited)
                        .First();

                    if(secondRelation != null)
                    {
                        secondRelation.RelationStatus = RelationStatus.Accepted;
                        dao.Update(relation);
                    }

                }

                else if (action.ToLower() == FriendshipsConsts.Delete.ToLower())
                {
                    var relations = dao.QueryFriendships()
                        .Where(x =>
                        (x.User1Id == fromUserId && x.User2Id == toUserId)
                        || (x.User1Id == toUserId && x.User2Id == fromUserId)
                        );

                    foreach(var rel in relations)
                    {
                        if (rel.RelationStatus == RelationStatus.Deleted)
                            continue;

                        rel.RelationStatus = RelationStatus.Deleted;
                        dao.Update(rel);
                    }
                }

                else
                {
                    throw new ArgumentOutOfRangeException(nameof(action));
                }
            }
        }

        public RelationStatus? RelationShipStatus(int user1Id, int user2Id)
        {
            using (var dbc = new EFRandevouDAL.RandevouBusinessDbContext())
            {
                var dao = new FriendshipDao(dbc);
                var relation = dao.QueryFriendships().FirstOrDefault(x =>
                 (x.User1Id == user1Id && x.User2Id == user2Id && x.RelationStatus != RelationStatus.Deleted)
                 || (x.User1Id == user2Id && x.User2Id == user1Id && x.RelationStatus != RelationStatus.Deleted));

                return relation?.RelationStatus;
            }
        }

        private bool RelationExists(FriendshipDao dao, int user1Id, int user2Id)
        {
            return dao.QueryFriendships().Any(x =>
             (x.User1Id == user1Id && x.User2Id == user2Id && x.RelationStatus != RelationStatus.Deleted)
             || (x.User1Id == user2Id && x.User2Id == user1Id && x.RelationStatus != RelationStatus.Deleted));
             
        }
    }
}
