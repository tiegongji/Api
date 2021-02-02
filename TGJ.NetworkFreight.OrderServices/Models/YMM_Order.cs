using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    public class YMM_Order
    {
        [Key]
        // [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public long? id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transportOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string internalOrderNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? transportOrderStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? start { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? end { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? totalFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string depositDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? depositAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? serviceFee { get; set; }
        /// <summary>
        /// 1300.00/已付款到收款方
        /// </summary>
        public string arriveDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiptDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preOilDesc { get; set; }
        /// <summary>
        /// 易红兵
        /// </summary>
        public string consignorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? consignorTel { get; set; }
        /// <summary>
        /// 张跃
        /// </summary>
        public string carrierName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? carrierTel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? protocolStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? protocolConfirmBtnStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? countdown { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? freezeFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? protocolType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? tradeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? myOrderFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? myProposeFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? carrierRegister { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? confirmContractSuppleButtonFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? applyPayFreightButtonFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? confirmReceiptButtonFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? confirmCargoButtonFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? confirmCargoButtonPopFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? confirmLoadButtonFlag { get; set; }
        /// <summary>
        /// 皖S2K527
        /// </summary>
        public string truckNumber { get; set; }
        /// <summary>
        /// 上海铁公鸡网络科技有限公司
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? uploadReceiptButtonFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? cancelOrderButtonFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? cancelOrderProcessFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string collectCarrierName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? collectCarrierTel { get; set; }
        /// <summary>
        /// 塔吊
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 0-0方
        /// </summary>
        public string goodsVolume { get; set; }
        /// <summary>
        /// 30-30吨
        /// </summary>
        public string goodsWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? loadTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? unloadTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string loadAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unloadAddress { get; set; }
        /// <summary>
        /// 高低板
        /// </summary>
        public string truckType { get; set; }
        /// <summary>
        /// 13.7米
        /// </summary>
        public string truckLength { get; set; }
        /// <summary>
        /// 一装一卸
        /// </summary>
        public string loadType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string arrivalPayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiptPayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualTotalFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualPreFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preFreightPayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualPreOilFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preOilFreightPayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualArriveFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? arriveFreightPayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualReceiptFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiptFreightPayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string actualCargoPayFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string actualCargoPayTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mergeActualExtraFreightItemDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string actualExtraPayItemList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recorderName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? recorderTel { get; set; }
        /// <summary>
        /// 已部分上传
        /// </summary>
        public string receiptIsUpload { get; set; }
        /// <summary>
        /// 未开票
        /// </summary>
        public string invoiceStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? completeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? serviceFeeAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string companyIdName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool riskFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool appealFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? refundedServiceAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string innerRemark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? agentFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agentMobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? jingxuanFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? puhuojingxuanFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? buyoutPriceFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? dealModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? depositRefundAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool showRefundWhenCancel { get; set; }
        /// <summary>
        /// 提示：您的申请提交后，司机如果在2小时内未处理，将由客服跟进处理。
        /// </summary>
        public string cancelWindowTips { get; set; }
        /// <summary>
        /// 1394.85/未支付
        /// </summary>
        public string cargoPayDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? oneCityFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gasCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string freightDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string longOilDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? orderSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? longFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string longFreightPayTime { get; set; }
        /// <summary>
        /// 1394.85/未支付
        /// </summary>
        public string longFreightDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? specialTruckFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? logoTab { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoiceFreightDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extraFreightDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string serviceFeeAmountDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualInvoiceFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? actualExtraFreight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool shortDistanceFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool sameWayFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool fastFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lineStartEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool mybCommodityMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderFreezeReason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? claimStatusFlag { get; set; }
    }
}
