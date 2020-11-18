using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderContext context;
        public OrderDetailRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public void Add(OrderDetail entity)
        {
            context.OrderDetail.Add(entity);
            context.SaveChanges();
        }
    }
}
