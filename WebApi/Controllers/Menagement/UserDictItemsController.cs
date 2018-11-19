using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.UsersService.DetailsDictionary;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Menagement
{
    [Route("api/[controller]")]
    public class UserDictItemsController : BasicController
    {
        [Route("ItemsTypes")]
        [HttpGet]
        public IActionResult GetDictionaryItemsTypesNames()
        {
            var service = GetService<IUserDetailsDictionaryService>();
            var names = service.GetTypesNames();
            return Ok(names.ToArray());
        }

        [Route("Interest/")]
        [HttpGet]
        public IActionResult GetInterests()
        {
            var service = GetService<IUserDetailsDictionaryService>();
            var interests = service.GetItems(RandevouData.Users.Details.UserDetailsTypesConsts.Interests);
            return Ok(interests.ToArray());
        }

        [Route("{typeName}/Items")]
        [HttpGet]
        public IActionResult GetItems(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName));

            var service = GetService<IUserDetailsDictionaryService>();
            var result = service.GetItems(typeName);
            return Ok(result);
        }

        [Route("Items/")]
        [HttpPost]
        public IActionResult PostItem([FromBody] DictionaryItemDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var service = GetService<IUserDetailsDictionaryService>();
            var result = service.AddItem(dto);
            return Ok(result);
        }

        [Route("Items/{id:int}/Enable")]
        [HttpPost]
        public IActionResult EnableItem(int id)
        {
            var service = GetService<IUserDetailsDictionaryService>();
            service.EnableItem(id);
            return Ok();
        }

        [Route("Items/{id:int}/Disable")]
        [HttpPost]
        public IActionResult DisableItem(int id)
        {
            var service = GetService<IUserDetailsDictionaryService>();
            service.DisableItem(id);
            return Ok();
        }
        [Route("Items/{id:int}/")]
        [HttpPatch]
        public IActionResult UpdateItem(int id, [FromBody] DictionaryItemDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (!dto.Id.HasValue)
                throw new ArgumentNullException(nameof(dto.Id));

            var service = GetService<IUserDetailsDictionaryService>();
            service.UpdateItem(dto);
            return Ok();
        }
    }
}
