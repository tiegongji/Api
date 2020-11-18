using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Interface
{
  public  interface IOrderReceiptImageRepository
    {
        void Add(OrderReceiptImage entity);
    }
}
