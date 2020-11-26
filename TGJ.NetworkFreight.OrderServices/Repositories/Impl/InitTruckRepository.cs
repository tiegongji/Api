using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class InitTruckRepository: IInitTruckRepository
    {
        public OrderContext context;
        public InitTruckRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<dynamic> GetList()
        {
            return context.InitTruck.Where(a => a.IsValid == true).ToList().Select(a => new { a.Length });
        }
    }
}
