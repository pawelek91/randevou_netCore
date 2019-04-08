using BusinessServices.AuthenticationService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers.Auth;

namespace WebApi.Controllers
{
    public abstract class BasicController : Controller
    {
        internal static T GetService<T>() where T : class
        {
            var service = BusinessServicesProvider.GetService<T>();
            if (service == null)
                throw new Exception("Brak serwisu dla " + typeof(T).FullName);

            return service;
        }

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

    [BasicAuth]
    public abstract class BasicBusinessAuthController : BasicController
    {
    }
}
