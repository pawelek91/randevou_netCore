using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandevouData.Messages;

namespace EFRandevouDAL.Messages
{
    public class MessagesDao : BasicDao<Message>
    {
        public IQueryable<Message> QueryMessages()
        {
            return dbc.Messages.AsQueryable();
        }
    }
}
