using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.FriendshipService
{
    public interface IFriendshipService
    {
        void SendFriendshipRequest(int fromUserId, int toUserId);
        void UpdateFriendshipStatus(int fromUserId, int toUserId, string action);
        int[] GetFriends(int userId);
        int[] GetFriendshipRequests(int userId);
    }
}
