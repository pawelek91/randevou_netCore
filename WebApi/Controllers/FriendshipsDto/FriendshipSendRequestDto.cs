﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.FriendshipsDto
{
    public class FriendshipSendRequestDto
    {
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }
}
