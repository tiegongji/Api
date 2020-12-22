using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.Dtos.BankCardService
{
    public class BankCardDto
    { 
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string? CardNumber { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// 预留姓名
        /// </summary>
        public string? Name { get; set; }
    }
}
