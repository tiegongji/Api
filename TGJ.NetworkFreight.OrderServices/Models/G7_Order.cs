using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    public class G7_Order
    {
        [Key]
        public long? id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipperOrgroot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supplyHash { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? totalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 常德市武陵区 到 南通市通州区
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string senderTel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string routeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string senderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string carType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? receiptTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string routeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string carLength { get; set; }
        /// <summary>
        /// 苏FH5295
        /// </summary>
        public string licenseNumber { get; set; }
        /// <summary>
        /// 江苏南通市通州区基地
        /// </summary>
        public string toPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cancelReason { get; set; }
        /// <summary>
        /// 塔吊
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 湖南常德新机
        /// </summary>
        public string startPlace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? orderSource { get; set; }
        /// <summary>
        /// 朱经理
        /// </summary>
        public string consignee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string consigneeTel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiveCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoiceTitleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipperOrgcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string toPlaceCode { get; set; }
        /// <summary>
        /// 工业品及机械设备
        /// </summary>
        public string goodsType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? shippingPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? intendedShippingTime { get; set; }
        /// <summary>
        /// 铁公鸡
        /// </summary>
        public string sender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supplyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? schedulingStrategy { get; set; }
        /// <summary>
        /// 高光明
        /// </summary>
        public string driverName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualShippingTime { get; set; }
        /// <summary>
        /// 无项目
        /// </summary>
        public string projectName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string projectId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string startPlaceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? status { get; set; }
    }
}
