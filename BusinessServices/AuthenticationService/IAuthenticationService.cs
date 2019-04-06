using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.AuthenticationService
{
    public interface IAuthenticationService
    {
        string LoginUser(int userId, string password);
        void RegisterUser(int userId, string password);
        bool ApiKeyProperly(string apiKey);
    }
}
