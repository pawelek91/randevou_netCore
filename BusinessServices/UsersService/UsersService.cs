using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.UsersService
{
	public class UsersService : IUsersService
    {
		private static List<User> _usersContainer;  
        public UsersService()
        {
			if(_usersContainer == null)
			{
				_usersContainer = new List<User>();
				_usersContainer.Add(new User("ela", 'f', new DateTime(1990, 12, 14)));
				_usersContainer.Add(new User("ala", 'f', new DateTime(1980, 11, 13)));
				_usersContainer.Add(new User("ola", 'f', new DateTime(1970, 10, 12)));
				_usersContainer.Add(new User("olek", 'm', new DateTime(1960, 9, 11)));
			}
        }

		public User GetUser(int id)
		{
			var user = _usersContainer.Where(x => x.Id == id).FirstOrDefault();
			return user;
		}

		public IQueryable<User> QueryUsers()
		{
			return _usersContainer.AsQueryable();
		}

		int IUsersService.Add(string userName, char gender, DateTime birthDate)
		{
			var user = new User(userName, gender, birthDate);
			_usersContainer.Add(user);
			return user.Id;
		}

		void IUsersService.Delete(int id)
		{
			var user = _usersContainer.Where(x => x.Id == id).FirstOrDefault();
			if (user == null)
				throw new ArgumentOutOfRangeException("brak usera o takim id");

			_usersContainer.Remove(user);
		}

		void IUsersService.Update(int id, string userName, char? gender, DateTime? birthdate)
		{
			var user = _usersContainer.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
                throw new ArgumentOutOfRangeException("brak usera o takim id");

			if (!string.IsNullOrWhiteSpace(userName))
				user.Name = userName;

			if (gender.HasValue)
				user.Gender = gender.Value;

			if (birthdate.HasValue && birthdate.Value != default(DateTime))
				user.BirthDate = birthdate.Value;
		}
	}
}
