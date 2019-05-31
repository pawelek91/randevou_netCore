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
            using (var dbc = new RandevouBusinessDbContext())
            {
                var dao = new UsersDao(dbc);
                var user = dao.GetUserWithDetails(id);
                var userDto = mapper.Map<User, UserDetailsDto>(user);
                var dictionaryService = BusinessServicesProvider.GetService<IUserDetailsDictionaryService>();

                userDto.EyesColor = dictionaryService.GetUserEyesColor(user.UserDetails.Id);
                userDto.HairColor = dictionaryService.GetUserHairColor(user.UserDetails.Id);
                userDto.Interests = dictionaryService.GetUsersInterests(user.UserDetails.Id);
                return userDto;
            }
        }


        public void UpdateUserDetails(int userId, UserDetailsDto dto)
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var dao = new UsersDao(dbc);
                var detailsDao = new DetailsDictionaryDao(dbc);
                var user = dao.GetUserWithDetails(userId);

                if (user == null)
                    throw new ArgumentOutOfRangeException(string.Format("Brak usera {0}", userId));

                UpdateDetails(user.UserDetails, dto);
                
                dbc.SaveChanges();
            }

            UpdateDetailsDictionaryItems(dto);
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

        private void UpdateDetailsDictionaryItems(UserDetailsDto dto)
        {
            using (var dbc = new RandevouBusinessDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var detailsDao = new DetailsDictionaryDao(dbc);
                var details = usersDao.GetUserWithDetails(dto.UserId).UserDetails;

                var detailsDictionaryService = BusinessServicesProvider.GetService<IUserDetailsDictionaryService>();

                if (dto.Interests?.Count() == 0)
                {
                    var existingDetails = details.DetailsItemsValues.Select(x => x.UserDetailsDictionaryItemId).ToArray();
                    foreach (var interestId in existingDetails)
                    {
                        var interestEntity = detailsDao.QueryDictionaryValues()
                            .Where(x => x.UserDetailsDictionaryItemId == interestId && x.UserDetailsId == details.Id)
                            .SingleOrDefault();

                        if (interestEntity != null)
                            detailsDao.DeleteItemValue(interestEntity);
                    }
                }
                else if (dto.Interests?.Count()>0)
                {
                    if(dto.UserId == default(int))
                    {
                        if (details.User == null)
                            throw new ArgumentNullException(nameof(dto.UserId));

                        dto.UserId = details.User.Id;
                    }
                    var existingDetails = details.DetailsItemsValues.Select(x => x.UserDetailsDictionaryItemId);
                    var interestsIds = detailsDictionaryService.GetInterestsIds();
                    var userInteresIds = interestsIds.Where(x => existingDetails.Contains(x));


                    var detailsToDelete = userInteresIds.Where(x => !dto.Interests.Contains(x));
                    var detailsToAdd = dto.Interests.Where(x => !userInteresIds.Any(y => y == x));

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
                dbc.SaveChanges();
            }

            using (var dbc = new RandevouBusinessDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var detailsDao = new DetailsDictionaryDao(dbc);
                var details = usersDao.GetUserWithDetails(dto.UserId).UserDetails;
                if (dto.EyesColor.HasValue)
                {
                    var eyesColorsIds = detailsDao.QueryDictionary().Where(x =>
                        x.DetailsType.Equals(UserDetailsTypesConsts.EyesColor, StringComparison.CurrentCultureIgnoreCase)
                        ).Select(x => x.Id).ToArray();

                    var userEyesColor = detailsDao.QueryDictionaryValues()
                    .Where(x => eyesColorsIds.Contains(x.UserDetailsDictionaryItemId)
                    && x.UserDetailsId == dto.UserId && x.Value)
                    .FirstOrDefault();


                    
                    if (userEyesColor?.UserDetailsDictionaryItemId != dto.EyesColor.Value)
                    {

                        if (userEyesColor?.UserDetailsDictionaryItemId != null && userEyesColor.UserDetailsDictionaryItemId != dto.EyesColor.Value)
                            detailsDao.DeleteItemValue(userEyesColor);

                        var eyesColorEntity = new UsersDetailsItemsValues()
                        {
                            UserDetailsDictionaryItemId = dto.EyesColor.Value,
                            UserDetailsId = details.Id,
                            Value = true,
                        };

                        detailsDao.AddItemValue(eyesColorEntity);
                    }
                }
            }

            using (var dbc = new RandevouBusinessDbContext())
            {
                var usersDao = new UsersDao(dbc);
                var detailsDao = new DetailsDictionaryDao(dbc);
                var details = usersDao.GetUserWithDetails(dto.UserId).UserDetails;
                // wspolne dla hair-color i eyes-color
                if (dto.HairColor.HasValue)
                {
                    var hairColorsIds = detailsDao.QueryDictionary().Where(x =>
                    x.DetailsType.Equals(UserDetailsTypesConsts.HairColor, StringComparison.CurrentCultureIgnoreCase)
                    ).Select(x => x.Id).ToArray();


                    var userHairColor = detailsDao.QueryDictionaryValues()
                        .Where(x => hairColorsIds.Contains(x.UserDetailsDictionaryItemId)
                        && x.UserDetailsId == dto.UserId && x.Value)
                        .FirstOrDefault();

                    if (userHairColor?.UserDetailsDictionaryItemId != dto.HairColor.Value)
                    { 

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
        }
    }

