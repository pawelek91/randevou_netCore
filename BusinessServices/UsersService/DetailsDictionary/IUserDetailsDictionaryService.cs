using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
