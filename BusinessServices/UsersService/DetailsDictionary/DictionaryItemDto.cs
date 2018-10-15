using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.UsersService.DetailsDictionary
{
    public class DictionaryItemDto
    {
        public int? Id { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ObjectType { get; set; }
    }
}
