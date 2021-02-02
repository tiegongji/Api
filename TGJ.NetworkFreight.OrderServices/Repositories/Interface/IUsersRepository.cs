using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Interface
{
   public interface IUsersRepository
    {
        User Get(int id);
    }
}
