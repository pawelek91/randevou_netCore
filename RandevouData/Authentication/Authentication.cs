using System;
using System.Collections.Generic;
using System.Text;

namespace RandevouData.Authentications
{
    public class Authentication
    {
        public int UserId { get; set; }
        public DateTime LogoutDate { get; private set; }
        public Guid AuthKey { get; private set; }

        public void Refresh()
        {
            this.LogoutDate = DateTime.Now.AddMinutes(15);
        }

        public void Logout()
        {
            this.LogoutDate = DateTime.MinValue;
        }
    }
}
