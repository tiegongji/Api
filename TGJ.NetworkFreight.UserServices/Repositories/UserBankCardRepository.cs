using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Context;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    /// <summary>
    /// 用户银行卡仓储实现
    /// </summary>
    public class UserBankCardRepository : IUserBankCardRepository
    {
        public UserContext UserContext;
        public UserBankCardRepository(UserContext UserContext)
        {
            this.UserContext = UserContext;
        }
        public void Create(UserBankCard UserBankCard)
        {
            UserContext.UserBankCard.Add(UserBankCard);
            UserContext.SaveChanges();
        }

        public void Delete(UserBankCard UserBankCard)
        {
            UserBankCard.IsDelete = true;
            UserContext.UserBankCard.Update(UserBankCard);
            UserContext.SaveChanges();
        }

        public UserBankCard GetUserBankCardById(int userId, int id)
        {
            return UserContext.UserBankCard.FirstOrDefault(a => a.Id == id && a.UserID == userId && a.IsDelete == false);
        }

        public IEnumerable<UserBankCard> GetUserBankCards(int userId)
        {
            return UserContext.UserBankCard.Where(a => a.UserID == userId && a.IsDelete == false).ToList();
        }
    }
}
