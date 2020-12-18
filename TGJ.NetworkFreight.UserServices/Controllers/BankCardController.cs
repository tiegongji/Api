using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Utils;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Services;

namespace TGJ.NetworkFreight.UserServices.Controllers
{
    /// <summary>
    /// 用户银行卡控制器
    /// </summary>
    [Route("BankCards")]
    [ApiController]
    public class BankCardController : ControllerBase
    {
        private readonly IUserBankCardService UserBankCardService;

        public BankCardController(IUserBankCardService UserBankCardService)
        {
            this.UserBankCardService = UserBankCardService;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="UserBankCard"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<UserBankCard> PostUserBankCard(UserBankCard UserBankCard)
        {
            UserBankCardService.Create(UserBankCard);
            return CreatedAtAction("GeUserBankCard", new { id = UserBankCard.Id }, UserBankCard);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<UserBankCard>> GetUserBankCards(int userId)
        {
            var models = UserBankCardService.GetUserBankCards(userId).ToList();

            models.ForEach(x =>
            {
                x.CardNumber = Util.ReplaceWithSpecialChar(x.CardNumber);
            });

            return models;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{id}")]
        public ActionResult<UserBankCard> GetUserBankCardById(int userId, int id)
        {
            return UserBankCardService.GetUserBankCardById(userId, id);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/{id}")]
        public ActionResult<UserBankCard> DeleteUserBankCard(int userId, int id)
        {
            var UserBankCard = UserBankCardService.GetUserBankCardById(userId, id);

            if (UserBankCard == null)
            {
                return NotFound(UserBankCard);
            }

            UserBankCardService.Delete(UserBankCard);

            return UserBankCard;
        }
    }
}
