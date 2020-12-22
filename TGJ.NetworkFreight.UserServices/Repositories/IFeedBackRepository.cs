using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    public interface IFeedBackRepository
    {
        void AddUpLoadFile(UpLoadFile entity);
        void Add(FeedBack entity, List<UpLoadFile> list = null);
    }
}
