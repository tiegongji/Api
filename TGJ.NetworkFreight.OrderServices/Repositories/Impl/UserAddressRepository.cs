using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class UserAddressRepository : IUserAddressRepository
    {
        public OrderContext context;
        public UserAddressRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public void Add(UserAddress entity)
        {
            entity.CreateTime = DateTime.Now;
            entity.LastUpdateTime = DateTime.Now;
            context.UserAddress.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id,int userid)
        {
            var entity = context.UserAddress.Where(a => a.ID == id&&a.UserID== userid).FirstOrDefault();

            if (entity==null )
            {
                throw new BizException("地址为空");
            }
            entity.IsValid = false;
            context.UserAddress.Update(entity);
            context.SaveChanges();
        }

        public IEnumerable<dynamic> GetList(int userid)
        {
            return context.UserAddress.Where(a => a.UserID == userid && a.IsValid == true);
          
        }
        public bool UserAddressExists(int id)
        {
            return context.UserAddress.Any(e => e.ID == id);
        }
    }
}
