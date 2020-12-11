using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.Models
{
    public class FeedBackDto
    {
        public int UserID { set; get; }
        public string Remark { set; get; }

        public List<UpLoadFile> imgs { set; get; }
    }
}
