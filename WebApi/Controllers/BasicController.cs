using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public abstract class BasicController : Controller
    {
        protected static T GetService<T>() where T : class
        {
            var service = BusinessServicesProvider.GetService<T>();
            if (service == null)
                throw new Exception("Brak serwisu dla " + typeof(T).FullName);

            return service;
        }
    }
}
