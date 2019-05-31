using System;
using System.Collections.Generic;
using System.Text;
using RandevouData.Users.Details;

namespace BusinessServices.UsersService.DetailsDictionary
{
    public interface IUserDetailsDictionaryService
    {
        int AddItem(DictionaryItemDto dto);
        void UpdateItem(DictionaryItemDto dto);
        void DisableItem(int itemId);
        void EnableItem(int itemId);
        DictionaryItemDto[] GetItems(string typeName);
        string[] GetTypesNames();
        int? GetUserEyesColor(int userDetailsId);
        int? GetUserHairColor(int userDetailsId);
        int[] GetUsersInterests(int userDetailsId);
        UsersDetailsItemsValues GetDictionaryValue(int id);
        int GetEyesColorItemId(string color);
        int GetHairColorItemId(string color);
        int[] GetInterestsIds();
    }
}
