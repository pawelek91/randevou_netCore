using System;
using BusinessServices.UsersService;
using BusinessServices.MessageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

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

            string decodedAuthenticationToken = Encoding.UTF8.GetString(
                                                Convert.FromBase64String(key.ToString()));
            return int.Parse(decodedAuthenticationToken.Split(':')[0]);
        }


    }
}
