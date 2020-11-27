﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using TGJ.NetworkFreight.OrderServices.Services.Interface;
using Microsoft.Extensions.Configuration;
using TGJ.NetworkFreight.OrderServices.Extend;

namespace TGJ.NetworkFreight.OrderServices.Services.Impl
{
    public class OrderService : IOrderService
    {
        public readonly IInitCategoryRepository IInitCategoryRepository;
        public readonly IInitTruckRepository IInitTruckRepository;
        public readonly IOrderRepository IOrderRepository;
        public IConfiguration IConfiguration { get; }
        public OrderService(IInitCategoryRepository IInitCategoryRepository, IInitTruckRepository IInitTruckRepository, IOrderRepository IOrderRepository, IConfiguration IConfiguration)
        {
            this.IInitCategoryRepository = IInitCategoryRepository;
            this.IInitTruckRepository = IInitTruckRepository;
            this.IOrderRepository = IOrderRepository;
            this.IConfiguration = IConfiguration;
        }

        public IEnumerable<dynamic> GetInitCategoryList()
        {
            return IInitCategoryRepository.GetList();
        }

        public IEnumerable<dynamic> GetInitTruckList()
        {
            return IInitTruckRepository.GetList();
        }

        public void Add(OrderDetailDto entity)
        {
            IOrderRepository.Add(entity);
        }

        public IEnumerable<dynamic> GetList(int userid, int pageIndex, int pageSize,  int? status)
        {
            return IOrderRepository.GetList(userid, pageIndex, pageSize,status);
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

        public void UpdateCancel(OrderCancelDto entity)
        {
            IOrderRepository.UpdateCancel(entity);
        }

        public void UpdateCarrierUser(Order entity)
        {
            IOrderRepository.UpdateCarrierUser(entity);
        }

        public void Confirm(Order entity)
        {
            IOrderRepository.Confirm(entity);
        }

        public void UpdateMoney(Order entity)
        {
            IOrderRepository.UpdateMoney(entity);
        }

        public void UpdateLoading(Order entity)
        {
            IOrderRepository.UpdateLoading(entity);
        }

        public void UpdateUnLoading(OrderDto entity)
        {
            string accessKeyId = IConfiguration["Ali:accessKeyId"];
            string accessKeySecret = IConfiguration["Ali:accessKeySecret"];
            string EndPoint = IConfiguration["Ali:EndPoint"];
            string bucketName = IConfiguration["Ali:bucketName"];
            var list = new List<OrderReceiptImage>();
            foreach (var item in entity.imgs)
            {
                var filename = "TMS/" + Guid.NewGuid().ToString() + ".jpg";
                var res = ALiOSSHelper.Upload(filename, item.FileUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);
                var model = new OrderReceiptImage();
                model.FileUrl = filename;
                model.CreateTime = DateTime.Now;
                model.Type = 1;
                model.OrderNo = entity.OrderNo;
                list.Add(model);
            }
            entity.imgs = list;
            IOrderRepository.UpdateUnLoading(entity);
        }


        //public void UpdateCarrierUser(Order entity)
        //{
        //    var model = IOrderRepository.Get(entity.ID);
        //    if (model.UserID == entity.UserID)
        //        throw new Exception("订单不存在");
        //    model.CarrierUserID = entity.CarrierUserID;
        //    model.LastUpdateTime = DateTime.Now;
        //    IOrderRepository.Update(model);
        //}

        //public void UpdateMoney(Order entity)
        //{
        //    var model = IOrderRepository.Get(entity.ID);
        //    if (model.UserID == entity.UserID)
        //        throw new Exception("订单不存在");
        //    model.TotalAmount = entity.TotalAmount;
        //    model.LastUpdateTime = DateTime.Now;
        //    IOrderRepository.Update(model);
        //}
    }
}
