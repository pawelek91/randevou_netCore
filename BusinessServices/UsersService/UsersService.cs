using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessServices.MessageService;
using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users;

namespace BusinessServices.UsersService
{
	public class UserService : IUsersService
    {
        UsersDao dao;
		IMapper mapper;
        public UserService(IMapper mapper)
        {
            dao = new UsersDao();
			this.mapper = mapper;
        }
		public UserDto GetUser(int id)
		{
			var entity =dao.Get(id);
			var userDto =mapper.Map<User,UserDto>(entity);
            return userDto;
		}

		public IQueryable<User> QueryUsers()
		{
            return dao.QueryUsers();
		}

		int IUsersService.Add(string userName, char gender, DateTime birthDate)
		{
			var user = new User(userName,string.Empty,gender,birthDate);
            var id = dao.Insert(user);
            return id;
		}

		void IUsersService.Delete(int id)
		{
            var user = dao.Get(id);
			if (user == null)
				throw new ArgumentOutOfRangeException("brak usera o takim id");

            dao.Delete(user);
        }

        void IUsersService.Update(int id, string userName, char? gender, DateTime? birthdate)
		{
            var user = dao.Get(id);
            if (user == null)
                throw new ArgumentOutOfRangeException("brak usera o takim id");

			if (!string.IsNullOrWhiteSpace(userName))
				user.Name = userName;

			//if (gender.HasValue)
			//	user.UserGender = gender.Value;

			if (birthdate.HasValue && birthdate.Value != default(DateTime))
				user.BirthDate = birthdate.Value;
		}
	}
}
