using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace TGJ.NetworkFreight.Commons.Exceptions.Handlers
{
    /// <summary>
    /// 自定义业务异常处理，转换成为json格式
    /// </summary>
    public class BizExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // 1、判断异常是否BizException
            if (context.Exception is BizException bizException)
            {
                // 1.1 将异常转换成为json结果
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorNo = bizException.ErrorNo;
                exceptionResult.ErrorInfo = bizException.ErrorInfo;
                if (bizException.Infos != null)
                {
                    exceptionResult.infos = bizException.Infos;
                }
                context.Result = new JsonResult(exceptionResult);
            }
            else
            {
                // 1.2 处理其他类型异常Exception
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorNo = -1;
                exceptionResult.ErrorInfo = context.Exception.Message;

                // 1.3 包装异常信息进行异常返回
                context.Result = new JsonResult(exceptionResult);
            }
        }
    }
}
