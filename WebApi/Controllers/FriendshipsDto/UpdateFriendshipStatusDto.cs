using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.FriendshipsDto
{
    public class UpdateFriendshipStatusDto : FriendshipSendRequestDto
    {
        public string Action { get; set; }
    }
}
