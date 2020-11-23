using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper mapper;
        public OrderContext context;
        public OrderRepository(OrderContext _context, IMapper _mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
        }

        public void Add(OrderDetailDto model)
        {
            var entity = mapper.Map<OrderDetail>(model);
            var orderno = GetOrderNo();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    entity.OrderNo = orderno;
                    context.OrderDetail.Add(entity);
                    context.SaveChanges();

                    var order = new Order();
                    order.UserID = model.UserID;
                    order.OrderNo = orderno;
                    order.TradeStatus = 1;
                    order.CreateTime = DateTime.Now;
                    order.LastUpdateTime = DateTime.Now;
                    context.Order.Add(order);
                    context.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private string GetOrderNo()
        {
            var orderno = DateTime.Now.ToString("MMddhhssmm") + new Random().Next(100, 999);
            if (context.OrderDetail.Any(a => a.OrderNo == orderno))
            {
                return GetOrderNo();
            }
            return orderno;
        }


        public IEnumerable<dynamic> GetList(int userid, int? status)
        {
            return from o in context.Order
                   where o.UserID == userid && (status.HasValue ? (o.TradeStatus == status) : (1 == 1))
                   join detail in context.OrderDetail on o.OrderNo equals detail.OrderNo
                    into _order
                   from order in _order.DefaultIfEmpty()
                   join t in context.InitTruck on order.TruckID equals t.ID
                   into _truck
                   from truck in _truck.DefaultIfEmpty()
                   join c in context.InitCategory on order.CategoryID equals c.ID
                   into _catetory
                   from catetory in _catetory.DefaultIfEmpty()
                   select new
                   {
                       order.Weight,
                       order.StartDate,
                       o.CarrierUserID,
                       o.TradeStatus
                   };
        }


        public dynamic GetDetail(int userid, string OrderNo)
        {
            try
            {
                var res = (from o in context.Order
                           where o.UserID == userid && o.OrderNo == OrderNo
                           join detail in context.OrderDetail on o.OrderNo equals detail.OrderNo
                            into _order
                           from order in _order.DefaultIfEmpty()
                           join t in context.InitTruck on order.TruckID equals t.ID
                           into _truck
                           from truck in _truck.DefaultIfEmpty()
                           join c in context.InitCategory on order.CategoryID equals c.ID
                           into _catetory
                           from catetory in _catetory.DefaultIfEmpty()
                           select new
                           {
                               Length = truck != null && float.IsNaN(truck.Length) ? 0 : truck.Length,
                               Weight = order == null ? 0 : order.Weight,
                               StartDate = order == null ? "" : order.StartDate.ToString("yyyy-MM-dd"),
                               o.CarrierUserID,
                               o.TradeStatus
                           });
                if (res.Any())
                    return res.FirstOrDefault();
                return new { };
            }
            catch (Exception)
            {
                throw new Exception("订单不存在");
            }
        }

        public IEnumerable<Order> GetListByUid(int userid)
        {
            return context.Order.Where(a => a.UserID == userid).ToList();
        }
    }
}
