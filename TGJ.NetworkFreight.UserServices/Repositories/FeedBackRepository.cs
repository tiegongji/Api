using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Context;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    public class FeedBackRepository : IFeedBackRepository
    {
        public UserContext UserContext;
        public FeedBackRepository(UserContext UserContext)
        {
            this.UserContext = UserContext;
        }

        public void AddUpLoadFile(UpLoadFile entity)
        {
            UserContext.UpLoadFile.Add(entity);
            UserContext.SaveChanges();
        }

        public void Add(FeedBack entity,List<UpLoadFile> list = null)
        {
            using (var tran = UserContext.Database.BeginTransaction())
            {
                try
                {
                    var model = UserContext.FeedBack.Add(entity);
                    UserContext.SaveChanges();

                    if(list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.Type = 0;
                            item.Status = 1;
                            item.TypeID = model.Entity.ID;
                            item.CreateTime = entity.CreateTime;
                            UserContext.UpLoadFile.Update(item);
                        }
                        UserContext.SaveChanges();
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
