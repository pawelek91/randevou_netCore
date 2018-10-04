using System;
using BusinessServices.UsersService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers
{
	public class BasicController:Controller
    {
		private static readonly ServiceCollection _serviceCollection = new ServiceCollection();
		private static ServiceProvider _serviceProvider;
        
		public BasicController()
        {
			RegisterServices();
        }

        private void RegisterServices()
		{
			_serviceProvider = _serviceCollection.AddSingleton<IUsersService, UsersService>()
			.BuildServiceProvider();
		}
        
       
		protected T GetService<T>() where T: class
		{
			
			var service = _serviceProvider.GetService<T>();
			if (service == null)
				throw new Exception("Brak serwisu dla " + typeof(T).FullName);

			return service;
		}


    }
}
