using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EFRandevouDAL;
using EFRandevouDAL.Authentication;
using RandevouData.Authentication;
using RandevouData.Users;

namespace BusinessServices.AuthenticationService
{
    internal class AuthenticationService : IAuthenticationService
    {
        public int GetUserIdFromKey(string key)
        {
            if(key.StartsWith("Basic"))
            {
                key = key.Remove(0, "basic".Length+1);
            }
            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                                               Convert.FromBase64String(key.ToString()));
            return int.Parse(decodedAuthenticationToken.Split(':')[0]);
        }
        public bool ApiKeyProperly(string apiKey)
        {
            int identity;
            string passwd = string.Empty;
            byte[] bytes;
            try
            { 
                bytes = Convert.FromBase64String(apiKey);
           
                var decoded = Encoding.ASCII.GetString(bytes);
                var userPassPair = decoded.Split(new char[] { ':' }, 2);
                int.TryParse(userPassPair[0], out identity);
                passwd = HashPassword(userPassPair[1]);
            }
            catch
            {
                return false;
            }

            using (var dbc = new RandevouAuthDbContext())
            {
                var userLoginInfo = dbc.UserLogins.FirstOrDefault(x => x.UserId == identity && x.Password == passwd);
                if (userLoginInfo == null)
                    return false;

                return true;
            }
        }

        public string LoginUser(string userName, string password)
        {
            using (var dbc = new RandevouAuthDbContext())
            {
                int userId;
                string passwd = HashPassword(password);

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




            string passwd = HashPassword(password);

            var userLogin = new UserLogin
            {
                Login = user.Name,
                IsActive = true,
                Password = passwd,
                UserId = user.Id,
            };

            using (var dbc = new RandevouAuthDbContext())
            {
                if (dbc.UserLogins.Any(x => x.UserId == userId || x.Login == user.Name))
                    throw new ArgumentException("User already exists");

                var dao = new UsersLoginsDao(dbc);
                dao.AddUserLogin(userLogin);
            }
        }

        private string HashPassword(string passwd)
        {
            string result = string.Empty;
            try
            { 
                var passBytes = Encoding.ASCII.GetBytes(passwd);
                var sha1data = new SHA1CryptoServiceProvider().ComputeHash(passBytes);
                result = new ASCIIEncoding().GetString(sha1data);
            }
            catch { }
            return result;
        }
    }
}
