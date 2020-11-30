using Aliyun.OSS;
using System;

namespace TGJ.NetworkFreight.Commons.Extend
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
