﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons;
using TGJ.NetworkFreight.Commons.AutoMappers;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.Commons.MemoryCaches;
using TGJ.NetworkFreight.Commons.Users;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.BankCardService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.BankCardService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 用户银行卡控制器
    /// </summary>
    [Route("api/BankCard")]
    [ApiController]
    [Authorize]
    public class BankCardController : ControllerBase
    {
        private readonly IBankCardClient bankCardClient;
        private readonly ICertificationClient certificationClient;
        private readonly ICaching caching;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankCardClient"></param>
        /// <param name="certificationClient"></param>
        /// <param name="caching"></param>
        public BankCardController(IBankCardClient bankCardClient, ICertificationClient certificationClient, ICaching caching)
        {
            this.bankCardClient = bankCardClient;
            this.certificationClient = certificationClient;
            this.caching = caching;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<dynamic> AddBankCard(SysUser sysUser, [FromForm] BankCardPo entity)
        {
            var flag = bankCardClient.Exists(sysUser.UserId, entity.CardNumber);

            if (flag)
                throw new BizException("银行卡已存在");

            var res = certificationClient.BankCertification(entity.CardNumber, entity.IdCard, entity.Name);

            JObject obj = JObject.Parse(res.ToString());

            var result = obj["errcode"].ToString();

            if (result != "00000")
                throw new BizException("认证失败");

            entity.UserID = sysUser.UserId;
            entity.IsSelf = true;
            entity.IsValid = true;
            entity.IsDelete = false;
            entity.CreateTime = DateTime.Now;

            var bankCard = bankCardClient.Add(entity);

            if (bankCard == null)
                return NotFound("添加失败");

            Task.Run(() =>
            {
                var bankCards = bankCardClient.GetList(sysUser.UserId);
                caching.Update($"BankCard{sysUser.UserId}", bankCards);
            });

            return Ok(bankCard);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<dynamic> Delete(SysUser sysUser, int id)
        {
            var bankCard = bankCardClient.Delete(sysUser.UserId, id);

            if (bankCard == null)
                return NotFound("删除对象不存在");

            return Ok(bankCard);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> GetList(SysUser sysUser)
        {
            var cacheKey = $"BankCard{sysUser.UserId}";
            var bankCard = caching.Get(cacheKey);

            if (bankCard == null)
            {
                bankCard = bankCardClient.GetList(sysUser.UserId);

                if (bankCard == null)
                    return NotFound("未查到结果");

                Task.Run(() =>
                {
                    caching.Set(cacheKey, bankCard);
                });
            }

            return Ok(bankCard);
        }
    }
}
