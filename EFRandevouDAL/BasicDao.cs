using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RandevouData;

namespace EFRandevouDAL
{
    public abstract class BasicDao<TEntity> where TEntity : BasicRandevouObject
    {
        protected RandevouDbContext dbc;
        public BasicDao(RandevouDbContext dbc) { this.dbc = dbc; }
        
        public virtual int Insert(TEntity entity)
        {
                dbc.Add<TEntity>(entity);
                dbc.SaveChanges();
                return entity.Id;
            
        }

        public virtual void Update(TEntity entity)
        {
                dbc.Update(entity);
                dbc.SaveChanges();
            
        }

        public virtual void Delete(TEntity entity)
        {
                dbc.Remove(entity);
                dbc.SaveChanges();
            
        }

        public virtual TEntity Get(int id)
        {
                var entity = dbc.Find<TEntity>(id);
                return entity;
            
        }


    }
}
