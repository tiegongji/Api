﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.OrderSercive
{
    public class OrderDetailPo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { set; get; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { set; get; }
        /// <summary>
        /// 货物类型ID
        /// </summary>
        public int CategoryID { set; get; }
        /// <summary>
        /// 车型ID
        /// </summary>
        public int TruckID { set; get; }
        /// <summary>
        /// 装货地址ID
        /// </summary>
        public int DepartureAddressID { set; get; }
        /// <summary>
        /// 卸货地址ID
        /// </summary>
        public int ArrivalAddressID { set; get; }
        /// <summary>
        /// 装货日期
        /// </summary>
        public DateTime StartDate { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { set; get; }
    }
}
