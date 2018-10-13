﻿using EFRandevouDAL;
using EFRandevouDAL.Users;
using RandevouData.Users.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessServices.UsersService
{
    public partial class UserService 
    {
        public void UpdateUserDetails(int userId, UserDetailsDto dto)
        {
            using (var dbc = new RandevouDbContext())
            {
                var dao = new UsersDao(dbc);
                var user = dao.GetUserWithDetails(userId);

                if (user == null)
                    throw new ArgumentOutOfRangeException(string.Format("Brak usera {0}", userId));

                UpdateDetails(user.UserDetails, dto, dao);

                //user.UserDetails.Region = dto.Region;
                dbc.SaveChanges();
            }
        }

        private void UpdateDetails(UserDetails details, UserDetailsDto dto, UsersDao dao)
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

        private void UpdateDetailsDictionaryItems(UserDetails details, UserDetailsDto dto, UsersDao dao)
        {

            //if (dto.EyesColor.HasValue)
            //{

            //    var userEyeColorItem = details.DetailsDictionaryItems
            //                        .Where(x => x.DictionaryItem.DetailsType == UserDetailsTypes.EyesColor)
            //                        .Select(x => x.DictionaryItem).FirstOrDefault();

            //    if (userEyeColorItem == null)
            //    {
            //        throw new ArgumentOutOfRangeException(string.Format("Brak zdefiniowanego koloru oczu w bazie"));
            //    }

            //    var newEyesColor = dao.QueryUsersDetails()
            //        .Where(x => x.DetailsType == UserDetailsTypes.EyesColor && x.Id == dto.EyesColor.Value)
            //        .FirstOrDefault();

            //    if(newEyesColor == null)
            //        throw new ArgumentOutOfRangeException(string.Format("Brak zdefiniowanego koloru oczu ({0})", dto.EyesColor.Value));

            //    userEyeColorItem = newEyesColor;
            //}

            //if(dto.Gender.HasValue)
            //{
            //    var userGenderItem = details.DetailsDictionaryItems
            //                      .Where(x => x.DictionaryItem.DetailsType == UserDetailsTypes.Gender)
            //                      .Select(x=>x.DictionaryItem).FirstOrDefault();


            //if (genderItem == null)
            //    throw new ArgumentOutOfRangeException(string.Format("Brak zdefiniowanego koloru oczu ({0})", dto.Gender.Value));
            //}
        }
        }
    }

