using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Services;

namespace TGJ.NetworkFreight.UserServices.Controllers
{
    /// <summary>
    /// 地址控制器
    /// </summary>
    [Route("FeedBack")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService IFeedBackService;

        public FeedBackController(IFeedBackService IFeedBackService)
        {
            this.IFeedBackService = IFeedBackService;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult AddFeedBack(FeedBackDto entity)
        {
            IFeedBackService.Add(entity);
            return Ok("添加成功");
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpLoadFile")]
        public ActionResult<UpLoadFile> AddOrderReceiptImage(UpLoadFile entity)
        {
            IFeedBackService.UpLoadFile(entity);
            return Ok(entity);
        }
    }
}
