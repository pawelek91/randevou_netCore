using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.RequestDto
{
    public class RequestMessagesDto
    {
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
