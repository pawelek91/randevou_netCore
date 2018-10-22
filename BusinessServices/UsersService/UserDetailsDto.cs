using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.UsersService
{
    public class UserDetailsDto
    {
        public int UserId { get; set; }
        public int? Width { get; set; }
        public int? Heigth { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int? Tattos { get; set; }

        #region dictionary
        public int? EyesColor { get; set; }
        public int? HairColor { get; set; }
        public int[] Interests { get; set; }
        #endregion
    }
}
