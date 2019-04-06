using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandevouData.Messages;

namespace EFRandevouDAL.Messages
{
    public class MessagesDao : BasicDao<Message>
    {
        public MessagesDao(RandevouBusinessDbContext dbc) : base(dbc)
        {

        }
        public IQueryable<Message> QueryMessages()
        {
            return dbc.Messages.AsQueryable();
        }

        public override Message Get(int id)
        {
            return dbc.Messages.Include(x => x.FromUser).Include(x => x.ToUser).FirstOrDefault(x=>x.Id==id);
        }
    }
}
