using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.UserServices.Models;
using UserAddress = TGJ.NetworkFreight.OrderServices.Models.UserAddress;

namespace TGJ.NetworkFreight.OrderServices.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }
        public DbSet<InitCategory> InitCategory { get; set; }
        public DbSet<InitTruck> InitTruck { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderFlow> OrderFlow { get; set; }
        public DbSet<OrderReceiptImage> OrderReceiptImage { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WxToken> WxToken { get; set; }
        public DbSet<UserTruck> UserTruck { get; set; }
        public DbSet<G7_Order> G7_Order { get; set; }
        public DbSet<YMM_Order> YMM_Order { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<AreaRelation> AreaRelation { get; set; }
    }
}
