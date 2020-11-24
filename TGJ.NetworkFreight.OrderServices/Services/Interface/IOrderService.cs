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
        void UpdateCancel(Order entity);
        void UpdateCarrierUser(Order entity);
        void UpdateUpload(Order entity, List<OrderReceiptImage> imgs);
        void UpdateMoney(Order entity);
        void UpdateLoading(Order entity);
        void UpdateUnLoading(Order entity, List<OrderReceiptImage> imgs);
    }
}
