using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users.Details;

namespace BusinessServices.UsersService.DetailsDictionary
{
    public class UserDetailsDictionaryService : IUserDetailsDictionaryService
    {
        IMapper mapper;
        private string[] _itemTypes = new string[]
        {
            UserDetailsTypes.EyesColor,
            UserDetailsTypes.Interests,
        };

        public UserDetailsDictionaryService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        private void ValidateItemType(string type)
        {
            if (!_itemTypes.Select(x => x.ToLower()).Contains(type.ToLower()))
                throw new ArgumentException("wrong item type");
        }

        public string[] GetTypesNames() => _itemTypes;

        public int AddItem(DictionaryItemDto dto)
        {
            ValidateItemType(dto.ItemType);
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name));

            using (var dbc = new RandevouDbContext())
            {
                var entity = new UserDetailsDictionaryItem()
                {
                    DetailsType = dto.ItemType,
                    DisplayName = (!string.IsNullOrEmpty(dto.DisplayName)) ? dto.DisplayName : dto.Name,
                    Name = dto.Name,
                    ObjectType = dto.ItemType == UserDetailsTypes.Interests ? "boolean" : "text",
                };
                var dao = new DetailsDictionaryDao(dbc);
                var id = dao.Insert(entity);
                return id;
            }
        }

        public void DisableItem(int itemId)
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);
                var entity = dao.Get(itemId);
                entity.IsDeleted = true;
                dao.Update(entity);
            }
        }

        public void EnableItem(int itemId)
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);
                var entity = dao.Get(itemId);
                entity.IsDeleted = false;
                dao.Update(entity);
            }
        }

        public DictionaryItemDto[] GetItems(string typeName)
        {
            ValidateItemType(typeName);

            using (var dbc = new RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);
                var details = dao.QueryDictionary().Where(x => x.DetailsType.Equals(typeName, StringComparison.CurrentCultureIgnoreCase) && !x.IsDeleted).ToArray();
                var dto = mapper.Map<UserDetailsDictionaryItem[], DictionaryItemDto[]>(details);
                return dto;
            }
        }

        public void UpdateItem(DictionaryItemDto dto)
        {
            if (!dto.Id.HasValue)
                throw new ArgumentNullException(nameof(dto.Id));

            if (!string.IsNullOrWhiteSpace(dto.ItemType) || !string.IsNullOrWhiteSpace(dto.ObjectType))
                throw new ArgumentException("Nie można zmienić typu elementu");

            using (var dbc = new RandevouDbContext())
            {
                var dao = new DetailsDictionaryDao(dbc);
                var entity = dao.Get(dto.Id.Value);

                if (!string.IsNullOrEmpty(dto.Name))
                    entity.Name = dto.Name;

                if (!string.IsNullOrEmpty(dto.DisplayName))
                    entity.DisplayName = dto.DisplayName;

                dao.Update(entity);
            }

        }
    }
}
