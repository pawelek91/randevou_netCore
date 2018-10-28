using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users;
using RandevouData.Users.Details;

namespace BusinessServices.UsersFinderService
{
    public class UserFinderService : IUserFinderService
    {
        public int[] FindUsers(SearchQueryDto dto)
        {
            using (var dbc = new RandevouDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var userDetialsDao = new DetailsDictionaryDao(dbc);
                int[] detailsIds = null;
                var query = QueryBasicData(dto, usersDao, out var basicQueryChanged);

                if (basicQueryChanged)
                {
                    int[] typedUsersIds = query.Select(x => x.Id).ToArray();

                    if (!typedUsersIds.Any())
                        return typedUsersIds;

                    else
                    {
                        detailsIds = QueryAdditionalValues(typedUsersIds, dto, usersDao, userDetialsDao);
                    }
                }

                else
                {
                    detailsIds = QueryAdditionalValues(null, dto, usersDao, userDetialsDao);
                }

                var usersIds = usersDao.QueryUserDetails()
                    .Where(x => detailsIds.Contains(x.Id))
                    .Select(x => x.UserId).ToArray();

                return usersIds;
            }
        }

        private IQueryable<User> QueryBasicData(SearchQueryDto dto, UsersDao usersDao, out bool queryChanged)
        {
            var query = usersDao.QueryUsers();
            queryChanged = false;

            if (!String.IsNullOrEmpty(dto.Name))
                query = query.Where(x => x.Name.Contains(dto.Name));


            if (dto.AgeFrom.HasValue)
                query = query.Where(x => x.BirthDate.Year <= DateTime.Now.Year - dto.AgeFrom.Value);


            if (dto.AgeTo.HasValue)
                query = query.Where(x => x.BirthDate.Year >= DateTime.Now.Year - dto.AgeTo.Value);


            if (dto.Gender.HasValue)
                query = query.Where(x => x.Gender == dto.Gender.Value);


            if (!String.IsNullOrEmpty(dto.Name) || dto.AgeFrom.HasValue || dto.AgeTo.HasValue || dto.Gender.HasValue)
                queryChanged = true;

            return query;
        }

        private int[] QueryAdditionalValues(int[] usersIds, SearchQueryDto dto, UsersDao usersDao, DetailsDictionaryDao dao)
        {
            IQueryable<UserDetails> usersDetailsQuery = usersDao.QueryUserDetails();
            if (usersIds != null)
                usersDetailsQuery = usersDetailsQuery.Where(x => usersIds.Contains(x.UserId));

            if (dto.Tatoos.HasValue)
                if (dto.Tatoos.Value == true)
                    usersDetailsQuery = usersDetailsQuery.Where(x => x.Tattos > 0);

            if (!String.IsNullOrEmpty(dto.Region))
                usersDetailsQuery = usersDetailsQuery.Where(x => x.Region.Contains(dto.Region));

            if (!String.IsNullOrEmpty(dto.City))
                usersDetailsQuery = usersDetailsQuery.Where(x => x.Region.Contains(dto.City));

            int[] filteredIds = null;

            if (dto.Tatoos.HasValue || !string.IsNullOrEmpty(dto.Region) || !string.IsNullOrEmpty(dto.City))
                filteredIds = usersDetailsQuery.Select(x => x.Id).ToArray();

            return QueryDictionaryValues(filteredIds,dto, dao);
        }

        private int[] QueryDictionaryValues(int[] filteredIds, SearchQueryDto dto, DetailsDictionaryDao dao)
        {
            var query = dao.QueryDictionaryValues();

            if (filteredIds != null)
                query = query.Where(x => filteredIds.Contains(x.UserDetailsId));

            if(dto.EyesColor.HasValue)
            {
                query = query.Where(x => x.UserDetailsDictionaryItemId == dto.EyesColor && x.Value);
            }

            if(dto.HairColor.HasValue)
            {
                query = query.Where(x => x.UserDetailsDictionaryItemId == dto.HairColor && x.Value);
            }

            if(dto.InterestIds?.Length > 0 )
            {
                query = query.Where(x => dto.InterestIds.Contains(x.UserDetailsDictionaryItemId));
               //query=query.Where(x=> dto.InterestIds.Select(y=> y).Any(y=> y == x.UserDetailsDictionaryItemId)
            }

            var userDetailsIds = query.Select(x => x.UserDetailsId).ToArray();
            return userDetailsIds;
        }
    }
}
