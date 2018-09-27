namespace EaseSource.AnDa.SMT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using EaseSource.AnDa.SMT.Web.MvcExtensions;
    using Microsoft.AspNetCore.Mvc;

    public abstract class SMTControllerBase : ControllerBase
    {
        public SMTControllerBase()
        {
        }

        /// <summary>
        /// 返回一个SuccessObjectResult用于HTTP返回操作
        /// </summary>
        /// <param name="value">Success的返回结构中content中要包含的内容</param>
        /// <returns>SuccessObjectResult对象</returns>
        [NonAction]
        public virtual SuccessObjectResult Success(object value)
        {
            return new SuccessObjectResult(value);
        }

        /// <summary>
        /// 返回一个SuccessObjectResult用于HTTP返回操作
        /// </summary>
        /// <returns>SuccessObjectResult对象，content属性的内容为null</returns>
        [NonAction]
        public virtual SuccessObjectResult Success()
        {
            return Success(null);
        }

        /// <summary>
        /// 返回一个ErrorObjectResult用于HTTP返回操作
        /// </summary>
        /// <param name="errMsg">Error的返回结构中ErrMsg的内容</param>
        /// <returns>ErrorObjectResult对象</returns>
        [NonAction]
#pragma warning disable CA1716 // Identifiers should not match keywords
        public virtual ErrorObjectResult Error(string errMsg)
#pragma warning restore CA1716 // Identifiers should not match keywords
        {
            return new ErrorObjectResult(errMsg);
        }

        /// <summary>
        /// 返回一个ErrorObjectResult用于HTTP返回操作
        /// </summary>
        /// <param name="errCode">错误代码</param>
        /// <param name="errMsg">错误消息</param>
        /// <param name="errLevel">错误级别</param>
        /// <returns>ErrorObjectResult对象</returns>
        [NonAction]
#pragma warning disable CA1716 // Identifiers should not match keywords
        public virtual ErrorObjectResult Error(int errCode, string errMsg, int errLevel)
#pragma warning restore CA1716 // Identifiers should not match keywords
        {
            return new ErrorObjectResult(errCode, errMsg, errLevel);
        }
    }
}
