using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.FeedBackService;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserTruckService;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.FeedBackService
{
    /// <summary>
    /// 用户反馈微服务客户端
    /// </summary>
    [MicroClient("http", "UserServices")]
    public interface IFeedBackClient
    {
        /// <summary>
        /// 新增
        /// </summary>
        [PostPath("/FeedBack/Add")]
        public dynamic Add(FeedBakcPo entity);
    }
}
