using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.Commons.Middlewares
{
    /// <summary>
    /// 自定义异常中间件，对于系统异常的处理， 超时异常，异常。
    /// 拦截Response输出流，进行统一异常处理
    /// </summary>
    public class SystemExceptionHandlerderMiddleware
    {
        private readonly RequestDelegate _next;

        public SystemExceptionHandlerderMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// 执行异常处理(执行action无法拦截的异常)
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // 1、执行下一个中间件
                await _next(httpContext);

                /* var statusCode = httpContext.Response.StatusCode;
                 var msg = "";
                 if (statusCode == 401)
                 {
                     msg = "未授权";
                 }
                 else if (statusCode == 404)
                 {
                     msg = "未找到服务";
                 }
                 else if (statusCode == 502)
                 {
                     msg = "请求错误";
                 }
                 else if (statusCode != 200)
                 {
                     msg = httpContext.Response.Body.ToString();
                 }
                 if (!string.IsNullOrWhiteSpace(msg))
                 {
                     await HandleExceptionAsync(httpContext, statusCode, msg);
                 }*/
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, httpContext.Response.StatusCode, ex.Message);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            context.Response.ContentType = "application/json;charset=utf-8";

            // 1、异常结果转换成json格式输出
            dynamic warpResult = new ExpandoObject();
            warpResult.ErrorNo = "-1";
            warpResult.ErrorInfo = msg;

            // 2、异常json格式输出
            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, warpResult);
        }
    }
}
