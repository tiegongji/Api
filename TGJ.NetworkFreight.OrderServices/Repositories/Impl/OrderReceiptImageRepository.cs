using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class OrderReceiptImageRepository : IOrderReceiptImageRepository
    {
        public OrderContext context;
        public OrderReceiptImageRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public void Add(OrderReceiptImage entity)
        {
            context.OrderReceiptImage.Add(entity);
            context.SaveChanges();
        }
    }
}
