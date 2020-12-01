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
using System.Web;
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

        /// <summary>
        /// 身份证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public dynamic OCRIdCard(string image, string side)
        {
            string host = Configuration["AliCertification:OCRIdCarUrl"];
            string path = "/ocr/idcardocr";
            string method = "POST";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = "";
            string bodys = $"image={image}&side={side}";

            return Certification(host, path, method, appcode, querys, bodys);
        }

        /// <summary>
        /// 身份证实名
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public dynamic RealNameCertification(string idCard, string name)
        {
            string host = Configuration["AliCertification:IdCardUrl"];
            string path = "/idcard";
            string method = "GET";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = $"idCard={idCard}&name={name}";
            string bodys = "";

            return Certification(host, path, method, appcode, querys, bodys);
        }

        /// <summary>
        /// 银行卡实名认证
        /// </summary>
        /// <param name="bankCard"></param>
        /// <param name="idCard"></param>
        /// <param name="realName"></param>
        /// <returns></returns>
        public dynamic BankCertification(string bankCard, string idCard, string realName)
        {
            string host = Configuration["AliCertification:BankCardUrl"];
            string path = "/bankcard3";
            string method = "POST";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = $"bankcard={bankCard}&idcard={idCard}&realname={realName}";
            string bodys = "";

            return Certification(host, path, method, appcode, querys, bodys);
        }

        /// <summary>
        /// 银行卡OCR
        /// </summary>
        /// <param name="pic"></param>
        /// <returns></returns>
        public dynamic OCRBank(string pic)
        {
            string host = Configuration["AliCertification:OCRBankUrl"];
            string path = "/ocr/bank-card";
            string method = "POST";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = "";
            string bodys = $"pic={pic}";

            return Certification(host, path, method, appcode, querys, bodys);
        }

        /// <summary>
        /// 驾驶证识别
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="type">1:正面/2:反面</param>
        /// <returns></returns>
        public dynamic OCRDriver(string pic, string type)
        {
            string host = Configuration["AliCertification:DriverCardUrl"];
            string path = "/ocr/driving-license";
            string method = "POST";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = "";
            string bodys = $"pic={pic}&type={type}";

            return Certification(host, path, method, appcode, querys, bodys);
        }

        /// <summary>
        /// 行驶证识别
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public dynamic OCRVehicle(string pic, string type)
        {
            string host = Configuration["AliCertification:VehicleUrl"];
            string path = "/ocr/vehicle-license";
            string method = "POST";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = "";
            string bodys = $"pic={pic}&type={type}";

            return Certification(host, path, method, appcode, querys, bodys);
        }

        /// <summary>
        /// 道路经营许可证识别
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public dynamic OCRPermit(string pic, string type)
        {
            string host = Configuration["AliCertification:PermitUrl"];
            string path = "/ai_market/ai_ocr_universal/dao_lu_jiao_tong/v1";
            string method = "POST";
            string appcode = Configuration["AliCertification:AppCode"];

            string querys = "";
            string bodys = $"IMAGE={pic}&type={type}";

            return Certification(host, path, method, appcode, querys, bodys);
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public dynamic Certification(string host, string path, string method, string appcode, string querys, string bodys)
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

            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

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

            var result = JsonConvert.DeserializeObject(json);

            return result;
        }
    }
}