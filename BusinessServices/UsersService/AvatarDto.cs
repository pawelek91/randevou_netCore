using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.UsersService
{
    public class AvatarDto
    {
        public int? UserId { get; set; }
        public string Base64Content { get; set; }
        public string ContentType { get; set; }
    }
}
