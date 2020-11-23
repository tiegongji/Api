using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Services
{
    /// <summary>
    /// 商品服务接口
    /// </summary>
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(string UserName);
        User GetUserById(int id);
        User GetUserByOpenId(string openId);
        void Create(User User);
        void Update(User User);
        void Delete(User User);
        bool UserExists(int id);
        bool UserNameExists(string UserName);
    }
}
