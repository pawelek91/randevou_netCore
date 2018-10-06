using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandevouData;

namespace EFRandevouDAL
{
    public abstract class BasicDao<TEntity> where TEntity : BasicRandevouObject
    {
        public BasicDao() { }
        
        public virtual int Insert(TEntity entity)
        {
            using (var dbc = new RandevouDbContext())
            {
                dbc.Add(entity);
                return entity.Id;
            }
        }

        public virtual void Update(TEntity entity)
        {
            using (var dbc = new RandevouDbContext())
            {
                dbc.Update(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var dbc = new RandevouDbContext())
            {
                dbc.Remove(entity);
            }
        }

        public virtual TEntity Get(int id)
        {
            using (var dbc = new RandevouDbContext())
            {
                var entity = dbc.Find<TEntity>(id);
                return entity;
            }
        }


    }
}
