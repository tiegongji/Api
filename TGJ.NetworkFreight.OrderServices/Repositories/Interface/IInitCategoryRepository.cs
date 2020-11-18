using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Interface
{
    public interface IInitCategoryRepository
    {
        IEnumerable<dynamic> GetList();
    }
}
