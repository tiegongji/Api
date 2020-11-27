using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TGJ.NetworkFreight.UserServices.WeChat.Lib;

namespace TGJ.NetworkFreight.UserServices.WeChat
{
    /// <summary>
    /// 微信小程序登录接口
    /// </summary>
    public class WeChatAppDecrypt
    {
        //public IConfiguration Configuration { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        //public WeChatAppDecrypt(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        public WeChatAppDecrypt()
        {
        }

        /// <summary>
        /// 获取OpenId和SessionKey的Json数据包
        /// </summary>
        /// <param name="code">客户端发来的code</param>
        /// <returns>Json数据包</returns>
        private string GetOpenIdAndSessionKeyString(string code, string appid, string secret)
        {
            //string temp = "https://api.weixin.qq.com/sns/jscode2session?" +
            //    "appid=" + Configuration["Wechat:APPID"]
            //    + "&secret=" + Configuration["Wechat:APPSECRET"]
            //    + "&js_code=" + code
            //    + "&grant_type=authorization_code";

            string temp = "https://api.weixin.qq.com/sns/jscode2session?" +
                "appid=" + appid
                + "&secret=" + secret
                + "&js_code=" + code
                + "&grant_type=authorization_code";

            return HttpService.Get(temp);
        }

        /// <summary>
        /// 反序列化包含OpenId和SessionKey的Json数据包
        /// </summary>
        /// <param name="loginInfo">Json数据包</param>
        /// <returns>包含OpenId和SessionKey的类</returns>
        public OpenIdAndSessionKey DecodeOpenIdAndSessionKey(WechatLoginInfo loginInfo)
        {
            var res = GetOpenIdAndSessionKeyString(loginInfo.code, loginInfo.appid, loginInfo.secret);
            OpenIdAndSessionKey oiask = JsonConvert.DeserializeObject<OpenIdAndSessionKey>(res);
            if (!String.IsNullOrEmpty(oiask.errcode))
                return null;
            return oiask;
        }


        /// <summary>
        /// 根据微信小程序平台提供的解密算法解密数据，推荐直接使用此方法
        /// </summary>
        /// <param name="loginInfo">登陆信息</param>
        /// <returns>用户信息</returns>
        public WechatUserInfo Decrypt(WechatLoginInfo loginInfo)
        {
            WechatUserInfo userInfo;
            if (loginInfo == null)
                return null;

            if (string.IsNullOrEmpty(loginInfo.code))
                return null;

            OpenIdAndSessionKey oiask = DecodeOpenIdAndSessionKey(loginInfo);

            if (oiask == null)
                return null;

            if (!String.IsNullOrWhiteSpace(oiask.openid) && !string.IsNullOrWhiteSpace(loginInfo.rawData))
            {
                userInfo = JsonConvert.DeserializeObject<WechatUserInfo>(loginInfo.rawData);
                userInfo.openId = oiask.openid;
                userInfo.unionId = oiask.unionId;
                return userInfo;
            }

            //if (!VaildateUserInfo(loginInfo, oiask))
            //    return null;

            userInfo = Decrypt(loginInfo.encryptedData, loginInfo.iv, oiask.session_key);
            userInfo.openId = oiask.openid;
            return userInfo;
        }
        /// <summary>
        /// 根据微信小程序平台提供的解密算法解密数据
        /// </summary>
        /// <param name="encryptedData">加密数据</param>
        /// <param name="iv">初始向量</param>
        /// <param name="sessionKey">从服务端获取的SessionKey</param>
        /// <returns></returns>
        public WechatUserInfo Decrypt(string encryptedData, string iv, string sessionKey)
        {
            WechatUserInfo userInfo;
            //创建解密器生成工具实例
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            //设置解密器参数
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            //格式化待处理字符串
            byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
            byte[] byte_iv = Convert.FromBase64String(iv);
            byte[] byte_sessionKey = Convert.FromBase64String(sessionKey);

            aes.IV = byte_iv;
            aes.Key = byte_sessionKey;
            //根据设置好的数据生成解密器实例
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                {
                    //        cs.Read(decryptBytes, 0, decryptBytes.Length);
                    //        cs.Close();
                    //        ms.Close();

                    byte[] xXml = Convert.FromBase64String(encryptedData);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }
                xBuff = decode2(ms.ToArray());
            }

            //解密
            //byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);

            //生成结果
            string result = Encoding.UTF8.GetString(xBuff);

            //反序列化结果，生成用户信息实例
            userInfo = JsonConvert.DeserializeObject<WechatUserInfo>(result);

            return userInfo;
        }
        private static byte[] decode2(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}
