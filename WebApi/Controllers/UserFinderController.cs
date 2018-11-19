using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersFinderService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserFinderController : BasicController
    {
        [HttpPost]
        public IActionResult FindUsers([FromBody]SearchQueryDto queryDto)
        {
            if (queryDto == null)
                queryDto = new SearchQueryDto();

            var service = GetService<IUserFinderService>();
            var result = service.FindUsers(queryDto);
            return Ok(result);
        }
    }
}
