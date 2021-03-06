﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Extend;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Repositories;

namespace TGJ.NetworkFreight.UserServices.Services
{
    public class FeedBackService : IFeedBackService
    {
        public readonly IFeedBackRepository IFeedBackRepository;
        public IConfiguration IConfiguration { get; }

        public FeedBackService(IFeedBackRepository IFeedBackRepository , IConfiguration IConfiguration)
        {
            this.IFeedBackRepository = IFeedBackRepository;
            this.IConfiguration = IConfiguration;
        }

        public void Add(FeedBackDto entity)
        {
            var model = new FeedBack();
            var now = DateTime.Now;
            model.UserID = entity.UserID;
            model.Remark = entity.Remark;
            model.CreateTime = DateTime.Now;
            //if (entity.imgs != null && entity.imgs.Count > 0)
            //    entity.imgs = UpLoadImage(entity.imgs);
            IFeedBackRepository.Add(model, entity.imgs);
        }

        //public List<UpLoadFile> UpLoadImage(List<UpLoadFile> list)
        //{
        //    string accessKeyId = IConfiguration["Ali:accessKeyId"];
        //    string accessKeySecret = IConfiguration["Ali:accessKeySecret"];
        //    string EndPoint = IConfiguration["Ali:EndPoint"];
        //    string bucketName = IConfiguration["Ali:bucketName"];
        //    string url = IConfiguration["Ali:url"];
        //    var now = DateTime.Now;
        //    foreach (var item in list)
        //    {
        //        var filename = "FB/" + now.Year + "/" + now.Month + "/" + now.Day + "/" + Guid.NewGuid().ToString() + ".jpg";
        //        var res = ALiOSSHelper.Upload(filename, item.FilePath, accessKeyId, accessKeySecret, EndPoint, bucketName);
        //        item.FilePath = url + filename;
        //    }
        //    return list;
        //}


        public void UpLoadFile(UpLoadFile entity)
        {
            string accessKeyId = IConfiguration["Ali:accessKeyId"];
            string accessKeySecret = IConfiguration["Ali:accessKeySecret"];
            string EndPoint = IConfiguration["Ali:EndPoint"];
            string bucketName = IConfiguration["Ali:bucketName"];
            string url = IConfiguration["Ali:url"];
            var now = DateTime.Now;
            var filename = "FB/" + now.Year + "/" + now.Month + "/" + now.Day + "/" + Guid.NewGuid().ToString() + ".jpg";
            var res = ALiOSSHelper.Upload(filename, entity.FilePath, accessKeyId, accessKeySecret, EndPoint, bucketName);

            entity.FilePath = url + filename;
            //entity.Type = -1;
            entity.CreateTime = DateTime.Now;
            IFeedBackRepository.AddUpLoadFile(entity);
        }
    }
}
