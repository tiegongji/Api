using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TGJ.NetworkFreight.CertificationServices.Dtos;
using TGJ.NetworkFreight.Commons.Exceptions;

namespace TGJ.NetworkFreight.CertificationServices.Services
{
    /// <summary>
    /// 认证服务实现
    /// </summary>
    public class CertificationService : ICertificationService
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public IConfiguration Configuration { get; }
        public CertificationService(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public decimal OCRIdCardCertification(string image, string side)
        {
            string host = Configuration["AliCertification:OCRIdCarUrl"];
            string path = "/ocr/idcardocr";
            string method = "POST";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = "";
            string bodys = $"image={image}&side={side}";

            return Certification(host, path, method, appcode, querys, bodys);
        }

        public decimal RealNameCertification(string idCard, string name)
        {
            string host = Configuration["AliCertification:IdCardUrl"];
            string path = "/idcard";
            string method = "GET";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = $"idCard={idCard}&name={name}";
            string bodys = "";

            return Certification(host, path, method, appcode, querys, bodys);
        }


        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public decimal Certification(string host, string path, string method, string appcode, string querys, string bodys)
        {
            string url = host + path;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }

            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new BizException("认证失败");
            }

            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var json = reader.ReadToEnd();

            dynamic result = JsonConvert.DeserializeObject(json);

            return result;
        }
    }
}