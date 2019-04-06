using System;
using System.Collections.Generic;
using System.Text;
using RandevouData.Users;

namespace RandevouData.Authentication
{
    public class UserLogin
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
