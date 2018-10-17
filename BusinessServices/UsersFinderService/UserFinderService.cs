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

                var query = QueryBasicData(dto, usersDao, out var queryChanged);

                if (queryChanged)
                {
                    int[] typedUsersIds = query.Select(x => x.Id).ToArray();

                    if (!typedUsersIds.Any())
                        return typedUsersIds;

                    else return QueryAdditionalValues(typedUsersIds, dto, userDetialsDao);
                }

                return QueryAdditionalValues(new int[0],dto, userDetialsDao);
            }
        }

        private IQueryable<User> QueryBasicData(SearchQueryDto dto, UsersDao usersDao, out bool queryChanged)
        {
            var query = usersDao.QueryUsers();
            queryChanged = false;

            if (!String.IsNullOrEmpty(dto.Name))
            { 
                query = query.Where(x => x.Name.Contains(dto.Name));
            }

            if (dto.AgeFrom.HasValue)
                query = query.Where(x => x.BirthDate.Year >= DateTime.Now.Year -  dto.AgeFrom.Value);

            if (dto.AgeTo.HasValue)
                query = query.Where(x => x.BirthDate.Year <= DateTime.Now.Year - dto.AgeFrom.Value);

            if (dto.Gender.HasValue)
                query = query.Where(x => x.Gender == dto.Gender.Value);


            if (!String.IsNullOrEmpty(dto.Name) || dto.AgeFrom.HasValue || dto.AgeTo.HasValue || dto.Gender.HasValue)
                queryChanged = true;
                
            return query;
        }

        private int[] QueryAdditionalValues(int[] usersIds, SearchQueryDto dto, DetailsDictionaryDao dao)
        {
            throw new NotImplementedException();

            //IQueryable<UsersDetailsItemsValues> query = dao.QueryDictionaryValues();
            //if(dto.Tatoos.HasValue)
            //    if(dto.Tatoos.Value == true)
            //        query=query.Where(x=>x.t)

            //dao.QueryDictionaryValues()
        }
    }
}
