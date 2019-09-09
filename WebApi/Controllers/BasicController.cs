using BusinessServices.AuthenticationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using WebApi.Controllers.Auth;

namespace WebApi.Controllers
{
    public abstract class BasicController : Controller
    {
        internal static T GetService<T>()
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
                if (string.IsNullOrWhiteSpace(key.ToString()))
                {
                    if(string.IsNullOrWhiteSpace(key = HttpContext.Request.Headers["Authorization"]))
                        throw new AuthenticationException("apiKey");
                }

                return GetService<IAuthenticationService>().GetUserIdFromKey(key);
            }
        }
    }
    [CustomExceptionFilter]
    [BasicAuth]
    public abstract class BasicBusinessAuthController : BasicController
    {
    }

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is ArgumentOutOfRangeException || context.Exception is ArgumentException)
            {
                context.Result = new StatusCodeResult(409);
                return;
            }
            else if (context.Exception is AuthenticationException)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            else
            { 
                base.OnException(context);
            }
        }
    }
}
