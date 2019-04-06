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
            using (var dbc = new RandevouBusinessDbContext())
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

                var usersIds = usersDao.QueryUserDetails();

                if (detailsIds != null)
                    usersIds = usersIds.Where(x => detailsIds.Contains(x.Id));

                return usersIds.Select(x => x.UserId).ToArray(); 
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
            bool queryChanged = false;

            if (usersIds != null)
            { 
                usersDetailsQuery = usersDetailsQuery.Where(x => usersIds.Contains(x.UserId));
                queryChanged = true;
            }

            if (dto.Tatoos.HasValue)
            { 
                if (dto.Tatoos.Value == true)
                    usersDetailsQuery = usersDetailsQuery.Where(x => x.Tattos > 0);
                else
                    usersDetailsQuery = usersDetailsQuery.Where(x => x.Tattos == 0);

                queryChanged = true;
            }

            if (!String.IsNullOrEmpty(dto.Region))
            {
                usersDetailsQuery = usersDetailsQuery.Where(x => x.Region.ToLower() == dto.Region.ToLower());
                queryChanged = true;
            }

            if (!String.IsNullOrEmpty(dto.City))
            {
                usersDetailsQuery = usersDetailsQuery.Where(x => x.City.ToLower() == dto.City.ToLower());
                queryChanged = true;
            }

            if (dto.HeightFrom.HasValue)
            { 
                usersDetailsQuery = usersDetailsQuery.Where(x => x.Heigth >= dto.HeightFrom.Value);
                queryChanged = true;
            }

            if (dto.HeightTo.HasValue)
            { 
                usersDetailsQuery = usersDetailsQuery.Where(x => x.Heigth <= dto.HeightTo.Value);
                queryChanged = true;
            }

            if (dto.WidthFrom.HasValue)
            { 
                usersDetailsQuery = usersDetailsQuery.Where(x => x.Width >= dto.WidthFrom.Value);
                queryChanged = true;
            }

            if (dto.WidthTo.HasValue)
            { 
                usersDetailsQuery = usersDetailsQuery.Where(x => x.Width <= dto.WidthTo.Value);
                queryChanged = true;
            }


            List<int>  filteredIds = null;

            if (queryChanged)
            {
                filteredIds = usersDetailsQuery.Select(x => x.Id).ToList();
            }
                

            return QueryDictionaryValues(filteredIds,dto, dao)?.ToArray();
        }

        private IEnumerable<int> QueryDictionaryValues(IEnumerable<int> filteredIds, SearchQueryDto dto, DetailsDictionaryDao dao)
        {
            if (filteredIds?.Count() == 0)
                return filteredIds;

            if (!(dto.EyesColor.HasValue || dto.HairColor.HasValue || dto.InterestIds?.Count() > 0))
                return filteredIds;

            bool queryChanged = false;
            var query = dao.QueryDictionaryValues();
        
            if (filteredIds != null)
            {
                query = query.Where(x => filteredIds.Contains(x.UserDetailsId));
                queryChanged = true;
            }

            if (dto.EyesColor.HasValue)
            {
                query = query.Where(x => x.UserDetailsDictionaryItemId == dto.EyesColor && x.Value);
                filteredIds = query.Select(x => x.UserDetailsId).ToList();
                queryChanged = true;
            }

            if(dto.HairColor.HasValue)
            {
                if(queryChanged)
                {
                    if (!filteredIds.Any())
                        return new int[0];

                    query = dao.QueryDictionaryValues().Where(x => 
                    filteredIds.Contains(x.UserDetailsId) && x.Value && x.UserDetailsDictionaryItemId == dto.HairColor );
                }
                else
                { 
                    query = query.Where(x => x.UserDetailsDictionaryItemId == dto.HairColor && x.Value);
                    queryChanged = true;
                }
                
                filteredIds = query.Select(x => x.UserDetailsId).ToArray();
                if (filteredIds != null && !filteredIds.Any())
                    return new int[0];
            }

            if (dto.InterestIds?.Length > 0)
            {
                int[] typedUsersIds = null;

                IQueryable<UsersDetailsItemsValues> interestQuery = null;

                if (queryChanged)
                {
                    interestQuery = dao.QueryDictionaryValues()
                        .Where(x => filteredIds.Contains(x.UserDetailsId));
                }
                else
                {
                    interestQuery = dao.QueryDictionaryValues();
                }


                var result = interestQuery.GroupBy(x => x.UserDetailsId)
                        .Select(x => new { userId = x.Key, interest = x.Select(y => y.UserDetailsDictionaryItemId) });

                typedUsersIds = result.Where(x =>
                                !(dto.InterestIds.Except(x.interest).Any())
                                ).Select(x => x.userId).ToArray();

                return typedUsersIds;
            }
            return filteredIds;
        }
    }
}
