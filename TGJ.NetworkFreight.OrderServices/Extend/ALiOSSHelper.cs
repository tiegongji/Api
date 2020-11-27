using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliyun.OSS;

namespace TGJ.NetworkFreight.OrderServices.Extend
{
    public class ALiOSSHelper
    {
        public static PutObjectResult Upload(string filename, string decodedString, string accessKeyId, string accessKeySecret, string EndPoint, string bucketName)
        {
            //创建OssClient实例。
            var client = new OssClient(EndPoint, accessKeyId, accessKeySecret);

            byte[] buffer = Convert.FromBase64String(decodedString.Split(',')[1]);
            System.IO.Stream iStream = new System.IO.MemoryStream(buffer);
            // 上传文件。
            PutObjectResult result = client.PutObject(bucketName, filename, iStream);
            return result;
        }
    }
}
