using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Services
{
    public interface IFeedBackService
    {
        void Add(FeedBackDto entity);
        void UpLoadFile(UpLoadFile entity);
    }
}
