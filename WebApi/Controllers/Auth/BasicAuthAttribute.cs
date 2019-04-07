using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using BusinessServices.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Controllers.Auth
{
    public class BasicAuthAttribute :Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var actionContext = context.HttpContext;
            var authService = BasicController.GetService<IAuthenticationService>();
            var authKey = actionContext.Request.Headers["Authorization"].ToString();

            if (authKey == string.Empty || !authService.ApiKeyProperly(authKey))
            {
                context.Result = new UnauthorizedResult();
            }
           
        }
    }
}
