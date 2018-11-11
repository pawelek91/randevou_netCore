using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.FriendshipService
{
    public class FriendshipService : IFriendshipService
    {
        public int[] GetFriends(int userId)
        {
            throw new NotImplementedException();
        }

        public int[] GetFriendshipRequests(int userId)
        {
            throw new NotImplementedException();
        }

        public void SendFriendshipRequest(int fromUserId, int toUserId)
        {
            throw new NotImplementedException();
        }

        public void UpdateFriendshipStatus(int fromUserId, int toUserId)
        {
            throw new NotImplementedException();
        }
    }
}
