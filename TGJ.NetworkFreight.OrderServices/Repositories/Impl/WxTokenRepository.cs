using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class WxTokenRepository : IWxTokenRepository
    {
        public OrderContext context;
        public WxTokenRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public WxToken Get(int id)
        {
            return context.WxToken.Where(a => a.Type == id).FirstOrDefault();
        }

        public void Update(WxToken entity)
        {
            context.WxToken.Update(entity);
            context.SaveChanges();
        }

    }
}