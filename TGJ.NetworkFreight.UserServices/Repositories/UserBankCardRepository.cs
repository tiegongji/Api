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
            UserContext.UserBankCards.Add(UserBankCard);
            UserContext.SaveChanges();
        }

        public void Delete(UserBankCard UserBankCard)
        {
            UserContext.UserBankCards.Remove(UserBankCard);
            UserContext.SaveChanges();
        }

        public UserBankCard GetUserBankCardById(int userId, int id)
        {
            return UserContext.UserBankCards.FirstOrDefault(a => a.Id == id && a.UserID == userId && a.IsValid == true);
        }

        public IEnumerable<UserBankCard> GetUserBankCards(int userId)
        {
            return UserContext.UserBankCards.Where(a => a.UserID == userId && a.IsValid == true).ToList();
        }
    }
}
