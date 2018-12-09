using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using EFRandevouDAL.Authentications;

namespace BusinessServices.Authentication
{
    public class AuthService
    {
        private readonly SHA1CryptoServiceProvider shaProvider;

        public AuthService()
        {
            shaProvider = new SHA1CryptoServiceProvider();
        }

        public void SetPassword(int userId, string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            var hashedPassword = shaProvider.ComputeHash(data);

            using (var dbc = new EFRandevouDAL.RandevouDbContext())
            {
                var dao = new AuthenticationDao(dbc);
                //dao.
            }
        }
    }
}
