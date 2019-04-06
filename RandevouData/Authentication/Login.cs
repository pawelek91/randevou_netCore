using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouData.Authentication
{
    public class Login
    {
        public int UserId { get; set; }
        public string ApiKey { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
