using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessServices.MessageService;
using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users;
using AutoMapper.QueryableExtensions;

namespace BusinessServices.UsersService
{
    public partial class UserService : IUsersService
    {

        IMapper mapper;

        public UserService(IMapper mapper)
        {

            this.mapper = mapper;
        }
        public UserDto GetUser(int id)
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var entity = dao.Get(id);
                var userDto = mapper.Map<User, UserDto>(entity);
                return userDto;
            }
        }

        public IEnumerable<UserDto> QueryUsers()
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var users = dao.QueryUsers().ToArray();
                var dto = mapper.Map<User[], UserDto[]>(users);

                if (dto.Count() < 2)
                    AddInitialUsers(dbc);
                return dto;
            }
        }

       

		public int Add(UserDto userDto)
		{
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            if (string.IsNullOrEmpty(userDto.Name))
                throw new ArgumentNullException(nameof(userDto.Name));

            if (!userDto.Gender.HasValue)
                throw new ArgumentNullException(nameof(userDto.Gender));

            if(!userDto.BirthDate.HasValue)
                throw new ArgumentNullException(nameof(userDto.BirthDate));

            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var user = new User(userDto.Name, userDto.DisplayName, userDto.Gender.Value, userDto.BirthDate.Value);
                var id = dao.Insert(user);
                return id;
            }
        }

        public void Delete(int id)
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var user = dao.Get(id);
                if (user == null)
                    throw new ArgumentOutOfRangeException("brak usera o takim id");
                dao.Delete(user);
            }
        }
        public void Update(UserDto userDto)
		{
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto));

            var id = userDto.Id;
            if (!(id.HasValue))
                throw new ArgumentNullException(nameof(userDto.Id));

            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var user = dao.Get(id.Value);
                if (user == null)
                    throw new ArgumentOutOfRangeException("brak usera o takim id");

                if (!string.IsNullOrWhiteSpace(userDto.Name))
                    user.Name = userDto.Name;

                if (userDto.Gender.HasValue)
                    user.Gender = userDto.Gender.Value;

                if (userDto.BirthDate.HasValue && userDto.BirthDate.Value != default(DateTime))
                    user.BirthDate = userDto.BirthDate.Value;

                dao.Update(user);
            }
        }

        private void AddInitialUsers(RandevouDbContext dbc)
        {
           

                var dao = new UsersDao(dbc);
                if (dao.QueryUsers().Count() > 2)
                    return;

                var user = new User("user1", string.Empty, 'M', new DateTime(1990, 12, 12));
                var user2 = new User("user2", string.Empty, 'F', new DateTime(1998, 12, 12));
                var user3 = new User("user3", string.Empty, 'M', new DateTime(1980, 12, 12));
                dao.Insert(user);
                dao.Insert(user2);
                dao.Insert(user3);
            
        }
    }
}
