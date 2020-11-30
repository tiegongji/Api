using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive
{
    /// <summary>
    /// 司机查询Dto
    /// </summary>
    public class DriversDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string? Phone { set; get; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
    }
}
