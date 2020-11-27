using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Repositories;

namespace TGJ.NetworkFreight.UserServices.Services
{
    /// <summary>
    /// 用户银行卡服务实现
    /// </summary>
    public class UserBankCardService : IUserBankCardService
    {
        public readonly IUserBankCardRepository UserBankCardRepository;

        public UserBankCardService(IUserBankCardRepository UserBankCardRepository)
        {
            this.UserBankCardRepository = UserBankCardRepository;
        }

        public void Create(UserBankCard UserBankCard)
        {
            UserBankCardRepository.Create(UserBankCard);
        }

        public void Delete(UserBankCard UserBankCard)
        {
            UserBankCardRepository.Delete(UserBankCard);
        }

        public UserBankCard GetUserBankCardById(int userId, int id)
        {
            return UserBankCardRepository.GetUserBankCardById(userId, id);
        }

        public IEnumerable<UserBankCard> GetUserBankCards(int userId)
        {
            return UserBankCardRepository.GetUserBankCards(userId);
        }
    }
}
