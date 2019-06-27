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
        UserDetailsDto GetUserWithDetails(int id);
        IEnumerable<UserDto> QueryUsers();
        IEnumerable<UserDto> QueryUsers(int[] ids);
        void UpdateUserDetails(int userId, UserDetailsDto dto);

        IEnumerable<UserAvatarDto> GetUsersAvatars(IEnumerable<int> userIds);

        void SetAvatar(int userId, System.IO.MemoryStream stream, string contentType);
    }
}
