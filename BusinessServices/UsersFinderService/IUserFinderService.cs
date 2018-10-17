using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.UsersFinderService
{
    public interface IUserFinderService
    {
        int[] FindUsers(SearchQueryDto dto);
    }
}
