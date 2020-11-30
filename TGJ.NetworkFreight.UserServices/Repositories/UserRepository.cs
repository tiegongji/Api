using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.UserServices.Context;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    /// <summary>
    /// 商品仓储实现
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public UserContext UserContext;
        public UserRepository(UserContext UserContext)
        {
            this.UserContext = UserContext;
        }
        public void Create(User User)
        {
            UserContext.Users.Add(User);
            UserContext.SaveChanges();
        }

        public void Delete(User User)
        {
            UserContext.Users.Remove(User);
            UserContext.SaveChanges();
        }

        public User GetUser(string UserName)
        {
            User user = UserContext.Users.First(u => u.Name == UserName);

            return user;
        }

        public User GetUserById(int id)
        {
            User user = UserContext.Users.Find(id);

            return user;
        }

        public IEnumerable<User> GetUserByKey(string key)
        {
            var user = UserContext.Users.Where(a => a.RoleName == 2 && (a.Name.Contains(key) || a.Phone.Contains(key))).ToList();

            return user;
        }

        public User GetUserByOpenId(string openId)
        {
            User user = UserContext.Users.FirstOrDefault(a => a.wx_OpenID == openId);

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return UserContext.Users.ToList();
        }

        public void Update(User User)
        {
            UserContext.Users.Update(User);
            UserContext.SaveChanges();
        }
        public bool UserExists(int id)
        {
            return UserContext.Users.Any(e => e.Id == id);
        }

        public bool UserNameExists(string UserName)
        {
            return UserContext.Users.Any(e => e.Name == UserName);
        }
    }
}
