using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class AreaRelationRepository : IAreaRelationRepository
    {
        public OrderContext context;
        public AreaRelationRepository(OrderContext _context)
        {
            this.context = _context;
        }
        public void Add(AreaRelation entity)
        {
            var obj = context.AreaRelation.Where(a => a.Type == entity.Type && a.OrderNo == entity.OrderNo && a.RelationOrderNo == entity.RelationOrderNo).FirstOrDefault();
            if (obj != null && obj.ID > 0)
            {
                obj.Status = entity.Status;
                entity.CreateTime = DateTime.Now;
                context.AreaRelation.Update(obj);
            }
            else
            {
                entity.CreateTime = DateTime.Now;
                context.AreaRelation.Add(entity);
            }
            context.SaveChanges();
        }

        public IEnumerable<AreaRelation> GetList(Expression<Func<AreaRelation, bool>> filter)
        {
            return context.AreaRelation.Where(filter);
        }
    }
}
