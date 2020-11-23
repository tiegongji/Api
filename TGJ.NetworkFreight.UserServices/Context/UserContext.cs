using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Context
{
    /// <summary>
    /// 用户服务上下文
    /// </summary>
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 用户地址
        /// </summary>
        public DbSet<UserAddress> UserAddresss { get; set; }

        /// <summary>
        /// 用户银行卡
        /// </summary>
        public DbSet<UserBankCard> UserBankCards { get; set; }

        /// <summary>
        /// 用户车辆
        /// </summary>
        public DbSet<UserTruck> UserTrucks { get; set; }
    }
}
