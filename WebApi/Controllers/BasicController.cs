using System;
using BusinessServices.UsersService;
using BusinessServices.MessageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using BusinessServices.AuthenticationService;

namespace WebApi.Controllers
{
	public abstract class BasicController:Controller
    {       
		public static T GetService<T>() where T: class
		{
			var service = BusinessServicesProvider.GetService<T>();
			if (service == null)
				throw new Exception("Brak serwisu dla " + typeof(T).FullName);

			return service;
		}

        protected int? LoggedUserId()
        {
            var key = HttpContext.Request.Headers["Authentication"];
            if (key.ToString() == null)
                throw new ArgumentNullException("apiKey");

            return GetService<IAuthenticationService>().GetUserIdFromKey(key);
        }


    }
}
