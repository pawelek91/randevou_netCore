using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Users.Details;

namespace EFRandevouDAL.Users
{
    public class DetailsDictionaryDao : BasicDao<UserDetailsDictionaryItem>
    {
        public DetailsDictionaryDao(RandevouBusinessDbContext dbc) : base(dbc)
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

        public UsersDetailsItemsValues GetValue(int id)
        {
            return dbc.UsersDetailsItemsValues.Find(id);
        }


        public void DeleteItemValue(UsersDetailsItemsValues entity)
        {
            dbc.UsersDetailsItemsValues.Remove(entity);
            dbc.SaveChanges();
        }

        public void AddItemValue(UsersDetailsItemsValues entity)
        {
            dbc.Add(entity);
            dbc.SaveChanges();
        }

    }
}
