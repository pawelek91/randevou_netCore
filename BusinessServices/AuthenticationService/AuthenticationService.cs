using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EFRandevouDAL;
using RandevouData.Authentication;
using RandevouData.Users;

namespace BusinessServices.AuthenticationService
{
    internal class AuthenticationService : IAuthenticationService
    {
        public bool ApiKeyProperly(string apiKey)
        {
            var bytes = Convert.FromBase64String(apiKey);
            var decoded = Encoding.ASCII.GetString(bytes);
            var userPassPair = decoded.Split(new char[] { ':' }, 2);
            int.TryParse(userPassPair[0], out var id);

            using (var dbc = new RandevouAuthDbContext())
            {
                var userLoginInfo = dbc.UserLogins.FirstOrDefault(x => x.UserId == id && x.Password == userPassPair[1])
                ?? throw new System.Security.Authentication.AuthenticationException("Login Failed");

                return true;
            }
        }

        public string LoginUser(string userName, string password)
        {
            using (var dbc = new RandevouAuthDbContext())
            {
                int userId;
                var passBytes = Encoding.ASCII.GetBytes(password);
                var sha1data = new SHA1CryptoServiceProvider().ComputeHash(passBytes);
                string passwd = new ASCIIEncoding().GetString(sha1data);

                userId = dbc.UserLogins.FirstOrDefault(x => x.Login == userName && x.Password == passwd)?.UserId
                    ??   throw new System.Security.Authentication.AuthenticationException("Login Failed");

                string authInfo = userId.ToString() + ":" + password;
                string key = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                return key;
            }
        }

        public void RegisterUser(int userId, string password)
        {
            User user;
            string userName = string.Empty;
            using (var dbc = new RandevouBusinessDbContext())
                user = dbc.Users.FirstOrDefault(x => x.Id == userId) 
                    ?? throw new ArgumentException("Wrong id");
            

            string authInfo = userName + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            var passBytes = Encoding.ASCII.GetBytes(password);
            var sha1data = new SHA1CryptoServiceProvider().ComputeHash(passBytes);

            var userLogin = new UserLogin
            {
                Login = user.Name,
                IsActive = true,
                Password = new ASCIIEncoding().GetString(sha1data),
                UserId = user.Id,
            };

            using (var dbc = new RandevouAuthDbContext())
            {
                if (dbc.UserLogins.Any(x => x.UserId == userId || x.Login == user.Name))
                    throw new ArgumentException("User already exists");

                dbc.UserLogins.Add(userLogin);
            }
        }
    }
}
