using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(string UserName);
        User GetUserById(int id);
        User GetUserByOpenId(string openId);
        User GetUserByKey(string key);
        void Create(User User);
        void Update(User User);
        void Delete(User User);
        bool UserExists(int id);
        bool UserNameExists(string UserName);
    }
}
