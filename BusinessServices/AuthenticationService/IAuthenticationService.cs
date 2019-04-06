using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.AuthenticationService
{
    public interface IAuthenticationService
    {
        //return basic auth key
        string LoginUser(string userName, string password);

        void RegisterUser(int userId,  string password);

        //check if api key is properly
        bool ApiKeyProperly(string apiKey);
    }
}
