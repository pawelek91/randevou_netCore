using System;
using System.Linq;

namespace BusinessServices.UsersService
{
    public interface IUsersService
    {
	    int Add(string userName, char gender, DateTime birthDate);
		void Delete(int id);
		void Update(int id, string userName, char? gender, DateTime? birthdate);
		User GetUser(int id);
		IQueryable<User> QueryUsers();
    }
}
