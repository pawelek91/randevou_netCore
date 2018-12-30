using BusinessServices.UsersService.DetailsDictionary;
using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users;
using RandevouData.Users.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessServices.UsersService
{
    public partial class UserService 
    {
        public UserDetailsDto GetUserWithDetails(int id)
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var user = dao.GetUserWithDetails(id);
                var userDto = mapper.Map<User, UserDetailsDto>(user);
                var dictionaryService = BusinessServicesProvider.GetService<IUserDetailsDictionaryService>();

                return userDto;
            }
        }


        public void UpdateUserDetails(int userId, UserDetailsDto dto)
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var detailsDao = new DetailsDictionaryDao(dbc);
                var user = dao.GetUserWithDetails(userId);

                if (user == null)
                    throw new ArgumentOutOfRangeException(string.Format("Brak usera {0}", userId));

                UpdateDetails(user.UserDetails, dto);
                UpdateDetailsDictionaryItems(user.UserDetails, dto, detailsDao);
                dbc.SaveChanges();
            }
        }

        private void UpdateDetails(UserDetails details, UserDetailsDto dto)
        {
            if (!String.IsNullOrEmpty(dto.City))
                details.City = dto.City;

            if (dto.Heigth.HasValue)
                details.Heigth = dto.Heigth.Value;

            if (!String.IsNullOrEmpty(dto.Region))
                details.Region = dto.Region;

            if (dto.Tattos.HasValue)
                details.Tattos = dto.Tattos.Value;

            if (dto.Width.HasValue)
                details.Width = dto.Width.Value;

        }

        private void UpdateDetailsDictionaryItems(UserDetails details, UserDetailsDto dto, DetailsDictionaryDao detailsDao)
        {
            var detailsDictionaryService = BusinessServicesProvider.GetService<IUserDetailsDictionaryService>();
            if(dto.Interests.Any())
            { 

                var existingDetails = details.DetailsItemsValues.Select(x => x.UserDetailsDictionaryItemId).ToArray();

                var detailsToDelete = existingDetails.Where(x => !dto.Interests.Contains(x));
                var detailsToAdd = dto.Interests.Where(x => !existingDetails.Any(y => y == x));

                foreach (var interestId in detailsToAdd)
                {
                    var entity = new UsersDetailsItemsValues()
                    {
                        UserDetailsId = details.Id,
                        UserDetailsDictionaryItemId = interestId,
                        Value = true,
                    };
                    detailsDao.AddItemValue(entity);
                }

                foreach(var interestId in detailsToDelete)
                {
                    var interestEntity = detailsDao.QueryDictionaryValues()
                        .Where(x => x.UserDetailsDictionaryItemId == interestId && x.UserDetailsId == details.Id)
                        .SingleOrDefault();

                    if (interestEntity != null)
                        detailsDao.DeleteItemValue(interestEntity);
                }
            }
            
            if(dto.EyesColor.HasValue)
            {
               var userEyesColorId =  detailsDictionaryService.GetUserEyesColor(details.Id);

               if (userEyesColorId.HasValue)
               {
                    var userEyesColorEntity = detailsDictionaryService.GetDictionaryValue(userEyesColorId.Value);

                    if (userEyesColorEntity?.UserDetailsDictionaryItemId!= null && userEyesColorEntity.UserDetailsDictionaryItemId != dto.EyesColor.Value)
                        detailsDao.DeleteItemValue(userEyesColorEntity);
               }

                //var eyesColorsIds = detailsDao.QueryDictionary().Where(x =>
                //x.DetailsType.Equals(UserDetailsTypesConsts.EyesColor, StringComparison.CurrentCultureIgnoreCase)
                //).Select(x => x.Id).ToArray();


                //var userEyesColor2 = detailsDao.QueryDictionaryValues()
                //    .Where(x => eyesColorsIds.Contains(x.UserDetailsDictionaryItemId)
                //    && x.UserDetailsId == dto.UserId)
                //    .FirstOrDefault();


                //if (userEyesColor?.UserDetailsDictionaryItemId != dto.EyesColor.Value)
                //    detailsDao.DeleteItemValue(userEyesColor);

                var eyesColorEntity = new UsersDetailsItemsValues()
                {
                    UserDetailsDictionaryItemId = dto.EyesColor.Value,
                    UserDetailsId = details.Id,
                    Value = true,
                };

                detailsDao.AddItemValue(eyesColorEntity);
            }

            // wspolne dla hair-color i eyes-color
            if (dto.HairColor.HasValue)
            {
                var hairColorsIds = detailsDao.QueryDictionary().Where(x =>
                x.DetailsType.Equals(UserDetailsTypesConsts.HairColor, StringComparison.CurrentCultureIgnoreCase)
                ).Select(x => x.Id).ToArray();


                var userHairColor = detailsDao.QueryDictionaryValues()
                    .Where(x => hairColorsIds.Contains(x.UserDetailsDictionaryItemId)
                    && x.UserDetailsId == dto.UserId)
                    .FirstOrDefault();

                if (userHairColor?.UserDetailsDictionaryItemId != null && userHairColor.UserDetailsDictionaryItemId != dto.HairColor.Value)
                    detailsDao.DeleteItemValue(userHairColor);

                var hairColorEntity = new UsersDetailsItemsValues()
                {
                    UserDetailsDictionaryItemId = dto.HairColor.Value,
                    UserDetailsId = details.Id,
                    Value = true,
                };

                detailsDao.AddItemValue(hairColorEntity);
            }
        }
        }
    }

