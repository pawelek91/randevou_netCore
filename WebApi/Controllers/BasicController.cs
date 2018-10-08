using System;
using BusinessServices.UsersService;
using BusinessServices.MessageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers
{
	public class BasicController:Controller
    {       
		public BasicController()
        {
        }

		protected T GetService<T>() where T: class
		{
			var service = BusinessServicesProvider.GetService<T>();
			if (service == null)
				throw new Exception("Brak serwisu dla " + typeof(T).FullName);

			return service;
		}


    }
}
