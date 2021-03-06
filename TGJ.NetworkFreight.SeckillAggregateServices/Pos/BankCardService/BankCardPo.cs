﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.BankCardService
{
    /// <summary>
    /// 用户银行卡添加
    /// </summary>
    public class BankCardPo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 是否本人
        /// </summary>
        public bool IsSelf { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDelete { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
