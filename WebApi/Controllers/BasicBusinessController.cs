using System;
using BusinessServices.UsersService;
using BusinessServices.MessageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using BusinessServices.AuthenticationService;
using WebApi.Controllers.Auth;

namespace WebApi.Controllers
{
    [BasicAuth]
	public abstract class BasicBusinessController: BasicController
    {       
		
        protected int? LoggedUserId
        {
            get
            { 
            var key = HttpContext.Request.Headers["Authentication"];
            if (key.ToString() == null)
                throw new ArgumentNullException("apiKey");

            return GetService<IAuthenticationService>().GetUserIdFromKey(key);
            }
        }


    }
}
