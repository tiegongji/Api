using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class OrderFlowRepository : IOrderFlowRepository
    {
        public OrderContext context;
        public OrderFlowRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public void Add(OrderFlow entity)
        {
            context.OrderFlow.Add(entity);
            context.SaveChanges();
        }
    }
}
