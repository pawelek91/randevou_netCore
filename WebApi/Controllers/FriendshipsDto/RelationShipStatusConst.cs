using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.FriendshipsDto
{
    public class RelationShipStatusConst
    {
        public const string None = nameof(None);
        public const string Friends = nameof(Friends);
        public const string Invited = nameof(Invited);
        public const string Created = nameof(Created);
    }
}
