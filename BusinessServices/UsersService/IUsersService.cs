using System;
using System.Linq;
using BusinessServices.MessageService;
using RandevouData.Users;
using System.Collections.Generic;

namespace BusinessServices.UsersService
{
    public interface IUsersService
    {
	    int Add(UserDto userDto);
		void Delete(int id);
		void Update(UserDto userDto);
		UserDto GetUser(int id);
		IEnumerable<UserDto> QueryUsers();
    }
}
