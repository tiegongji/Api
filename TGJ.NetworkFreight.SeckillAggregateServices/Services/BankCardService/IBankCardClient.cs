using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.BankCardService;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.BankCardService
{
    /// <summary>
    /// 用户银行卡微服务客户端
    /// </summary>
    [MicroClient("http", "UserServices")]
    public interface IBankCardClient
    {
        /// <summary>
        /// 新增地址
        /// </summary>
        [PostPath("/BankCards")]
        public dynamic Add(BankCardPo entity);

        /// <summary>
        /// 删除
        /// </summary>
        [DeletePath("/BankCards/{userId}/{id}")]
        public dynamic Delete(int userId, int id);

        /// <summary>
        /// 获取列表
        /// </summary>
        [GetPath("/BankCards/{userId}")]

        public dynamic GetList(int userId);
    }
}
