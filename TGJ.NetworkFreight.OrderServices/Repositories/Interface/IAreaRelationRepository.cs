using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Interface
{
    public interface IAreaRelationRepository
    {
        IEnumerable<AreaRelation> GetList(Expression<Func<AreaRelation, bool>> filter);
        void Add(AreaRelation entity);
    }
}
