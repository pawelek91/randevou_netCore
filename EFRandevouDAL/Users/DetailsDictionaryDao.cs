using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Users.Details;

namespace EFRandevouDAL.Users
{
    public class DetailsDictionaryDao : BasicDao<UserDetailsDictionaryItem>
    {
        public DetailsDictionaryDao(RandevouDbContext dbc) : base(dbc)
        {
        }

        public IQueryable<UserDetailsDictionaryItem> QueryDictionary()
        {
            return dbc.UserDetailsDictionary.AsQueryable<UserDetailsDictionaryItem>();
        }

        public IQueryable<UsersDetailsItemsValues> QueryDictionaryValues()
        {
            return dbc.UsersDetailsItemsValues.AsQueryable();
        }


        public void DeleteItemValue(UsersDetailsItemsValues entity)
        {
            dbc.UsersDetailsItemsValues.Remove(entity);
        }

        public void AddItemValue(UsersDetailsItemsValues entity)
        {
            dbc.Add(entity);
        }

    }
}
