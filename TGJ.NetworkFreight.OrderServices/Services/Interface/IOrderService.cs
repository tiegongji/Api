using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Services.Interface
{
    public interface IOrderService
    {
        IEnumerable<dynamic> GetInitCategoryList();
        IEnumerable<dynamic> GetInitTruckList();
        void Add(OrderDetailDto entity);
        IEnumerable<dynamic> GetList(int userid, int pageIndex, int pageSize, int? status);
        dynamic GetDetail(int userid, string OrderNo);
        OrderGatherDto GetOrderGather(int userid);
        OrderTurnoverDto GetOrderTurnover(int userid);
        OrderStateTurnoverDto GetOrderStateTurnover(int userid);
        void UpdateCancel(OrderCancelDto entity);
        void UpdateCarrierUser(Order entity);
        void Confirm(Order entity);
        void UpdateMoney(Order entity);
        void UpdateLoading(Order entity);
        void UpdateUnLoading(OrderDto entity);
        IEnumerable<dynamic> GetWayBillList(int userId, int pageIndex, int pageSize, int? status);
    }
}
