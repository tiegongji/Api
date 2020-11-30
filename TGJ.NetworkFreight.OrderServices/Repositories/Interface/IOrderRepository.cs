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
        void Add(OrderDetailDto entity);
        IEnumerable<dynamic> GetList(int userid, int pageIndex, int pageSize, int? status);
        IEnumerable<Order> GetListByUid(int userid);
        dynamic GetDetail(int userid, string OrderNo);
        Order Get(string id);
        void Update(Order entity);
        void UpdateCancel(OrderCancelDto entity);
        void UpdateCarrierUser(Order entity);
        void Confirm(Order entity);
        void UpdateMoney(Order entity);
        void UpdateLoading(Order entity);
        void UpdateUnLoading(OrderDto entity);
        IEnumerable<dynamic> GetWayBillList(int userId, int pageIndex, int pageSize, int? status);
    }
}
