using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.FeedBackService
{
    public class FeedBakcPo
    {
        public int UserID { set; get; }
        public string Remark { set; get; }

        public List<UpLoadFile> imgs { set; get; }
    }
}
