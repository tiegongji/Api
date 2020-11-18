using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using TGJ.NetworkFreight.OrderServices.Services.Interface;

namespace TGJ.NetworkFreight.OrderServices.Services.Impl
{
    public class OrderService : IOrderService
    {
        public readonly IInitCategoryRepository IInitCategoryRepository;
        public readonly IInitTruckRepository IInitTruckRepository;
        public readonly IOrderRepository IOrderRepository;
        public OrderService(IInitCategoryRepository IInitCategoryRepository, IInitTruckRepository IInitTruckRepository, IOrderRepository IOrderRepository)
        {
            this.IInitCategoryRepository = IInitCategoryRepository;
            this.IInitTruckRepository = IInitTruckRepository;
            this.IOrderRepository = IOrderRepository;
        }

        public IEnumerable<dynamic> GetInitCategoryList()
        {
            return IInitCategoryRepository.GetList();
        }

        public IEnumerable<dynamic> GetInitTruckList()
        {
            return IInitTruckRepository.GetList();
        }

        public void Add(OrderDetail entity)
        {
            IOrderRepository.Add(entity);
        }

        public IEnumerable<dynamic> GetList(int userid, int? status)
        {
            return IOrderRepository.GetList(userid, status);
        }

        public dynamic GetDetail(int userid, string OrderNo)
        {
            return IOrderRepository.GetDetail(userid, OrderNo);
        }

        public OrderGatherDto GetOrderGather(int userid)
        {
            var orders = IOrderRepository.GetListByUid(userid);
            var orderGather = new OrderGatherDto();
            orderGather.Dispatch = orders.Count(a => a.TradeStatus == 1);
            orderGather.Confirm = orders.Count(a => a.TradeStatus == 2);
            orderGather.Complete = orders.Count(a => a.TradeStatus == 3);

            return orderGather;
        }

        public OrderTurnoverDto GetOrderTurnover(int userid)
        {
            var orders = IOrderRepository.GetListByUid(userid);
            var orderTurnover = new OrderTurnoverDto();

            var now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, 1);
            var endTime = startTime.AddMonths(1).AddDays(-1);

            orderTurnover.MonthlyTurnover = orders.Where(a => a.CreateTime > startTime && a.CreateTime < endTime).Sum(a => a.TotalAmount);

            orderTurnover.TotalTurnover  = orders.Sum(a => a.TotalAmount);

            return orderTurnover;
        }
    }
}
