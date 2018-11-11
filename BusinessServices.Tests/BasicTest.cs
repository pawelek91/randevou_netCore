using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.Tests
{
    public abstract class BasicTest
    {
        public T GetService<T>()
        {
            var service = BusinessServicesProvider.GetService<T>();
            return service;
        }
        
    }
}
