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
        Order Get(int id);
        void Update(Order entity);
        void UpdateCancel(Order entity);
        void UpdateCarrierUser(Order entity);
        void UpdateUpload(Order entity, List<OrderReceiptImage> imgs);
        void UpdateMoney(Order entity);
        void UpdateLoading(Order entity);
        void UpdateUnLoading(Order entity, List<OrderReceiptImage> imgs);
    }
}
