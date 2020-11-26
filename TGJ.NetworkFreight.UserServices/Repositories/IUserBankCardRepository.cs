using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    /// <summary>
    /// 用户银行卡仓储接口
    /// </summary>
    public interface IUserBankCardRepository
    {
        IEnumerable<UserBankCard> GetUserBankCards(int userId);
        UserBankCard GetUserBankCardById(int id);
        void Create(UserBankCard UserBankCard);
        void Delete(UserBankCard UserBankCard);
    }
}
