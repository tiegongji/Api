using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.Commons.Users;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.AddressService;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.FeedBackService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.AddressService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.FeedBackService;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 地址控制器
    /// </summary>
    [Route("api/FeedBack")]
    [ApiController]
    [Authorize]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackClient IFeedBackClient;
        public FeedBackController(IFeedBackClient IFeedBackClient)
        {
            this.IFeedBackClient = IFeedBackClient;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult<dynamic> Add(FeedBakcPo entity)
        {
            return IFeedBackClient.Add(entity);
        }
    }
}
