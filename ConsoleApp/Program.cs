using System;
using FirstNetCoreAPp;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
			Person person = new Person() { Name = "hehe" };
            Console.WriteLine(person.Name);
        }
    }
}
