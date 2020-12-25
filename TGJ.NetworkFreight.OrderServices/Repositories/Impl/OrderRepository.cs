using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Extend;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using static TGJ.NetworkFreight.OrderServices.Models.Enum.EnumHelper;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper mapper;
        public OrderContext context;
        public IConfiguration IConfiguration { get; }
        public OrderRepository(OrderContext _context, IMapper _mapper, IConfiguration IConfiguration)
        {
            this.context = _context;
            this.mapper = _mapper;
            this.IConfiguration = IConfiguration;
        }

        public void Add(OrderDetailDto model)
        {
            var entity = mapper.Map<OrderDetail>(model);
            var orderno = GetOrderNo();
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    if (context.Users.Any(a => a.Id == model.UserID && a.HasAuthenticated == false))
                        throw new Exception("请先认证");

                    var DepartureAddress = context.UserAddress.Where(a => a.ID == model.DepartureAddressID).FirstOrDefault();
                    var ArrivalAddress = context.UserAddress.Where(a => a.ID == model.ArrivalAddressID).FirstOrDefault();

                    if (DepartureAddress == null)
                    {
                        throw new Exception("装货地址不能为空");
                    }
                    if (ArrivalAddress == null)
                    {
                        throw new Exception("卸货地址不能为空");
                    }

                    entity.OrderNo = orderno;
                    entity.Distance = Tool.GetDistance(DepartureAddress.TencentLat, DepartureAddress.TencentLng, ArrivalAddress.TencentLat, ArrivalAddress.TencentLng).ToDecimal();
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="status">3：调度中，4：已完成，33：回单确认</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetList(int userId, int pageIndex, int pageSize, int? status)
        {
            return (from o in context.Order
                    where o.UserID == userId && (status.HasValue ? (status == 33 ? o.TradeStatus == (int)EnumOrderStatus.Start && o.ActionStatus == (int)EnumActionStatus.Unloading : o.TradeStatus == status) : (1 == 1))
                    join detail in context.OrderDetail on o.OrderNo equals detail.OrderNo
                     into _order
                    from order in _order.DefaultIfEmpty()
                    join t in context.InitTruck on order.TruckID equals t.ID
                    into _truck
                    from truck in _truck.DefaultIfEmpty()
                    join c in context.InitCategory on order.CategoryID equals c.ID
                    into _catetory
                    from catetory in _catetory.DefaultIfEmpty()
                    join DepartureAddress in context.UserAddress on order.DepartureAddressID equals DepartureAddress.ID
                    into _DepartureAddress
                    from DepartureAddress_New in _DepartureAddress.DefaultIfEmpty()
                    join ArrivalAddress in context.UserAddress on order.ArrivalAddressID equals ArrivalAddress.ID
                    into _ArrivalAddress
                    from ArrivalAddress_New in _ArrivalAddress.DefaultIfEmpty()
                    orderby o.CreateTime descending
                    select new
                    {
                        order.OrderNo,
                        truck.Length,
                        order.Weight,
                        Date = order.StartDate.ToDate(),
                        order.Distance,
                        DepartureAddressName = DepartureAddress_New.Name,
                        DepartureAddress = DepartureAddress_New.Address,
                        ArrivalAddressName = ArrivalAddress_New.Name,
                        ArrivalAddress = ArrivalAddress_New.Address,
                        TradeStatusText = ((EnumOrderStatus)o.TradeStatus).GetDescriptionOriginal(),
                        o.TradeStatus,
                        o.ActionStatus,
                        o.TotalAmount
                    }).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }


        public dynamic GetDetail(int userId, string OrderNo)
        {
            try
            {
                var url = IConfiguration["Ali:url"];
                var arr = context.OrderReceiptImage.Where(a => a.OrderNo == OrderNo);
                var imgs = arr.Where(a => a.Type == 2).Select(b => b.FileUrl).ToList();
                var imgs2 = arr.Where(a => a.Type == 3).Select(b => b.FileUrl).ToList();
                var res = (from o in context.Order
                           where (o.UserID == userId || o.CarrierUserID == userId) && o.OrderNo == OrderNo
                           join detail in context.OrderDetail on o.OrderNo equals detail.OrderNo
                            into _order
                           from order in _order.DefaultIfEmpty()
                           join t in context.InitTruck on order.TruckID equals t.ID
                           into _truck
                           from truck in _truck.DefaultIfEmpty()
                           join c in context.InitCategory on order.CategoryID equals c.ID
                           into _catetory
                           from catetory in _catetory.DefaultIfEmpty()
                           join DepartureAddress in context.UserAddress on order.DepartureAddressID equals DepartureAddress.ID
                           into _DepartureAddress
                           from DepartureAddress_New in _DepartureAddress.DefaultIfEmpty()
                           join ArrivalAddress in context.UserAddress on order.ArrivalAddressID equals ArrivalAddress.ID
                           into _ArrivalAddress
                           from ArrivalAddress_New in _ArrivalAddress.DefaultIfEmpty()
                           join user in context.Users on o.CarrierUserID equals user.Id
                           into _user
                           from user_new in _user.DefaultIfEmpty()
                           select new
                           {
                               order.Name,
                               order.OrderNo,
                               truck.Length,
                               order.Weight,
                               Date = order.StartDate.ToDate(),
                               order.Distance,
                               DepartureAddressObject = new
                               {
                                   DepartureAddress_New.Name,
                                   DepartureAddress_New.ContactPhone,
                                   DepartureAddress_New.ContactPerson,
                                   DepartureAddress_New.Address
                               },
                               DArrivalAddressObject = new
                               {
                                   ArrivalAddress_New.Name,
                                   ArrivalAddress_New.ContactPhone,
                                   ArrivalAddress_New.ContactPerson,
                                   ArrivalAddress_New.Address
                               },
                               TradeStatusText = ((EnumOrderStatus)o.TradeStatus).GetDescriptionOriginal(),
                               o.TradeStatus,
                               o.ActionStatus,
                               order.Comment,
                               Driver = user_new == null ? null : new
                               {
                                   user_new.Name,
                                   user_new.Phone
                               },
                               o.TotalAmount,
                               imgs,
                               imgs2
                           });
                if (res.Any())
                    return res.FirstOrDefault();
                return new { };
            }
            catch (Exception ex)
            {
                throw new BizException("订单不存在");
            }
        }

        public IEnumerable<Order> GetListByUid(int userId, int roleName)
        {
            if (roleName == 1)
                return context.Order.Where(a => a.UserID == userId).ToList();
            else
                return context.Order.Where(a => a.CarrierUserID == userId).ToList();
        }


        public Order Get(string orderno)
        {
            return context.Order.Where(a => a.OrderNo == orderno).FirstOrDefault();
        }


        public void Update(Order entity)
        {
            context.Order.Update(entity);
            context.SaveChanges();
        }

        #region 物流端状态更新
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCancel(OrderCancelDto entity)
        {
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    var model = Get(entity.OrderNo);
                    if (model == null || model.UserID != entity.UserID)
                        throw new Exception("订单不存在");
                    model.TradeStatus = (int)EnumOrderStatus.Cancel;
                    model.LastUpdateTime = DateTime.Now;
                    context.Order.Update(model);
                    context.SaveChanges();


                    var orderFlow = new OrderFlow();
                    orderFlow.ActionStatus = (int)EnumOrderStatus.Cancel;
                    orderFlow.OrderNo = model.OrderNo;
                    orderFlow.CreateTime = DateTime.Now;
                    orderFlow.Type = (int)EnumType.Logistics;
                    orderFlow.Reason = entity.Reason;
                    orderFlow.Description = entity.Description;
                    context.OrderFlow.Add(orderFlow);
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
        /// <summary>
        /// 指定司机
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCarrierUser(Order entity)
        {
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    var model = Get(entity.OrderNo);
                    if (model == null || model.UserID != entity.UserID)
                        throw new Exception("订单不存在");
                    if (model.CarrierUserID > 0)
                    {
                        throw new Exception("该订单已有指定司机");
                    }
                    if (model.TradeStatus != (int)EnumOrderStatus.Waiting)
                    {
                        throw new Exception("订单状态错误");
                    }
                    model.CarrierUserID = entity.CarrierUserID;
                    model.LastUpdateTime = DateTime.Now;
                    model.TradeStatus = (int)EnumOrderStatus.Start;
                    context.Order.Update(model);
                    context.SaveChanges();


                    var orderFlow = new OrderFlow();
                    orderFlow.ActionStatus = (int)EnumOrderStatus.Start;
                    orderFlow.OrderNo = model.OrderNo;
                    orderFlow.CreateTime = DateTime.Now;
                    orderFlow.Type = (int)EnumType.Logistics;
                    context.OrderFlow.Add(orderFlow);
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

        /// <summary>
        /// 回单确认
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="imgs"></param>
        public void Confirm(Order entity)
        {
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    var model = Get(entity.OrderNo);
                    if (model == null || model.UserID != entity.UserID)
                        throw new Exception("订单不存在");
                    if (model.ActionStatus != (int)EnumActionStatus.Unloading)
                    {
                        throw new Exception("司机未上传回单");
                    }
                    model.LastUpdateTime = DateTime.Now;
                    model.TradeStatus = (int)EnumOrderStatus.Finish;
                    context.Order.Update(model);
                    context.SaveChanges();


                    var orderFlow = new OrderFlow();
                    orderFlow.ActionStatus = (int)EnumOrderStatus.Finish;
                    orderFlow.OrderNo = model.OrderNo;
                    orderFlow.CreateTime = DateTime.Now;
                    orderFlow.Type = (int)EnumType.Logistics;
                    context.OrderFlow.Add(orderFlow);
                    context.SaveChanges();

                    //foreach (var img in imgs)
                    //{
                    //    img.Type = (int)EnumType.Logistics;
                    //    img.OrderNo = model.OrderNo;
                    //    img.CreateTime = DateTime.Now;
                    //    context.OrderReceiptImage.Add(img);

                    //}
                    //context.SaveChanges();

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

        }

        #endregion

        #region 司机端
        /// <summary>
        /// 运单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="status">null：运单列表,2：货源列表</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetWayBillList(int userId, int pageIndex, int pageSize, int? status)
        {
            var arr = new string[] { "北京市", "上海市", "天津市", "重庆市" };
            return (from o in context.Order
                    where o.CarrierUserID == userId && (status.HasValue ? status == (int)EnumOrderStatus.Start ? o.TradeStatus == status && o.ActionStatus == 0 : (o.TradeStatus == status) : (o.ActionStatus > 0))
                    join detail in context.OrderDetail on o.OrderNo equals detail.OrderNo
                     into _order
                    from order in _order.DefaultIfEmpty()
                    join t in context.InitTruck on order.TruckID equals t.ID
                    into _truck
                    from truck in _truck.DefaultIfEmpty()
                    join c in context.InitCategory on order.CategoryID equals c.ID
                    into _catetory
                    from catetory in _catetory.DefaultIfEmpty()
                    join DepartureAddress in context.UserAddress on order.DepartureAddressID equals DepartureAddress.ID
                    into _DepartureAddress
                    from DepartureAddress_New in _DepartureAddress.DefaultIfEmpty()
                    join ArrivalAddress in context.UserAddress on order.ArrivalAddressID equals ArrivalAddress.ID
                    into _ArrivalAddress
                    from ArrivalAddress_New in _ArrivalAddress.DefaultIfEmpty()
                    orderby o.CreateTime descending
                    select new
                    {
                        order.OrderNo,
                        truck.Length,
                        order.Weight,
                        Date = order.StartDate.ToDateForList(),
                        order.Distance,
                        order.Name,
                        DepartureAddressName = DepartureAddress_New.Name,
                        // DepartureAddress = DepartureAddress_New.Province + DepartureAddress_New.City,
                        DepartureAddress = arr.Contains(DepartureAddress_New.Province) ? DepartureAddress_New.Province + DepartureAddress_New.County : DepartureAddress_New.Province + DepartureAddress_New.City,
                        ArrivalAddressName = ArrivalAddress_New.Name,
                        //ArrivalAddress = ArrivalAddress_New.Province + ArrivalAddress_New.Province,
                        ArrivalAddress = arr.Contains(ArrivalAddress_New.Province) ? ArrivalAddress_New.Province + ArrivalAddress_New.County : ArrivalAddress_New.Province + ArrivalAddress_New.City,
                        TradeStatusText = ((EnumActionStatus_Driver)o.ActionStatus).GetDescriptionOriginal(),
                        o.ActionStatus,
                        o.TotalAmount
                    }).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }



        #region 司机端状态更新
        /// <summary>
        /// 修改金额
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateMoney(Order entity)
        {
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    var model = Get(entity.OrderNo);
                    if (model == null || !(model.CarrierUserID == entity.UserID || model.UserID == entity.UserID))
                        throw new Exception("订单不存在");
                    if (model.TradeStatus != (int)EnumOrderStatus.Start && model.ActionStatus < (int)EnumActionStatus.Loading)
                    {
                        throw new Exception("未指定司机");
                    }
                    model.TotalAmount = entity.TotalAmount;
                    model.LastUpdateTime = DateTime.Now;
                    model.ActionStatus = (int)EnumActionStatus.Pay;
                    context.Order.Update(model);
                    context.SaveChanges();


                    var orderFlow = new OrderFlow();
                    orderFlow.ActionStatus = (int)EnumActionStatus.Pay;
                    orderFlow.OrderNo = model.OrderNo;
                    orderFlow.CreateTime = DateTime.Now;
                    orderFlow.Type = (int)EnumType.Driver;
                    orderFlow.Description = entity.UserID.ToString();
                    context.OrderFlow.Add(orderFlow);
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

        /// <summary>
        /// 装货
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateLoading(OrderDto param)
        {
            if (param.imgs == null || param.imgs.Count == 0)
            {
                throw new Exception("请上传图片");
            }
            var entity = mapper.Map<Order>(param);
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    var model = Get(entity.OrderNo);
                    if (model == null || model.CarrierUserID != entity.UserID)
                        throw new Exception("订单不存在");
                    if (model.ActionStatus != (int)EnumActionStatus.Pay)
                    {
                        throw new Exception("该订单已装货");
                    }
                    model.ActionStatus = (int)EnumActionStatus.Loading;
                    model.LastUpdateTime = DateTime.Now;
                    model.DepartureTime = DateTime.Now;
                    context.Order.Update(model);
                    context.SaveChanges();


                    var orderFlow = new OrderFlow();
                    orderFlow.ActionStatus = (int)EnumActionStatus.Loading;
                    orderFlow.OrderNo = model.OrderNo;
                    orderFlow.CreateTime = DateTime.Now;
                    orderFlow.Type = (int)EnumType.Driver;
                    context.OrderFlow.Add(orderFlow);
                    context.SaveChanges();


                    if (param.imgs.Count > 0)
                    {
                        foreach (var img in param.imgs)
                        {
                            img.Type = (int)EnumActionStatus.Loading;
                            //img.OrderNo = model.OrderNo;
                            img.CreateTime = DateTime.Now;
                            img.Status = 1;
                            context.OrderReceiptImage.Update(img);
                        }
                        context.SaveChanges();
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

        }

        /// <summary>
        /// 卸货
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUnLoading(OrderDto param)
        {
            if (param.imgs == null || param.imgs.Count == 0)
            {
                throw new Exception("请上传图片");
            }
            var entity = mapper.Map<Order>(param);
            using (var tran = context.Database.BeginTransaction())
            {
                try
                {
                    var model = Get(entity.OrderNo);
                    if (model == null || model.CarrierUserID != entity.UserID)
                        throw new Exception("订单不存在");
                    if (model.ActionStatus != (int)EnumActionStatus.Loading)
                    {
                        throw new Exception("请先装货");
                    }
                    model.ActionStatus = (int)EnumActionStatus.Unloading;
                    model.LastUpdateTime = DateTime.Now;
                    model.ArrivalTime = DateTime.Now;
                    context.Order.Update(model);
                    context.SaveChanges();


                    var orderFlow = new OrderFlow();
                    orderFlow.ActionStatus = (int)EnumActionStatus.Unloading;
                    orderFlow.OrderNo = model.OrderNo;
                    orderFlow.CreateTime = DateTime.Now;
                    orderFlow.Type = (int)EnumType.Driver;
                    context.OrderFlow.Add(orderFlow);
                    context.SaveChanges();

                    if (param.imgs.Count > 0)
                    {
                        foreach (var img in param.imgs)
                        {
                            img.Type = (int)EnumActionStatus.Unloading;
                            //img.OrderNo = model.OrderNo;
                            img.CreateTime = DateTime.Now;
                            img.Status = 1;
                            context.OrderReceiptImage.Update(img);

                        }
                        context.SaveChanges();
                    }
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

        }
        #endregion
        #endregion
    }
}
