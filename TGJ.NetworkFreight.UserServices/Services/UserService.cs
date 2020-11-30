using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Repositories;

namespace TGJ.NetworkFreight.UserServices.Services
{
    /// <summary>
    /// 商品服务实现
    /// </summary>
    public class UserService : IUserService
    {
        public readonly IUserRepository UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        public void Create(User User)
        {
            UserRepository.Create(User);
        }

        public void Delete(User User)
        {
            UserRepository.Delete(User);
        }

        public User GetUser(string UserName)
        {
            return UserRepository.GetUser(UserName);
        }

        public User GetUserById(int id)
        {
            return UserRepository.GetUserById(id);
        }

        public IEnumerable<User> GetUserByKey(string key)
        {
            return UserRepository.GetUserByKey(key);
        }

        public User GetUserByOpenId(string openId)
        {
            return UserRepository.GetUserByOpenId(openId);
        }

        public IEnumerable<User> GetUsers()
        {
            return UserRepository.GetUsers();
        }

        public void Update(User User)
        {
            UserRepository.Update(User);
        }

        public bool UserExists(int id)
        {
            return UserRepository.UserExists(id);
        }

        public bool UserNameExists(string UserName)
        {
            return UserRepository.UserNameExists(UserName);
        }
    }
}
