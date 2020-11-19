using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Interface
{
    public interface IOrderRepository
    {
        void Add(OrderDetail entity);
        IEnumerable<dynamic> GetList(int userid, int? status);
        IEnumerable<dynamic> GetListByUid(int userid);
        dynamic GetDetail(int userid, string OrderNo);
    }
}
