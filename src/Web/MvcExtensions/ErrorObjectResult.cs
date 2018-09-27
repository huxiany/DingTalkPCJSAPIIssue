namespace EaseSource.AnDa.SMT.Web.MvcExtensions
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 调用出错时返回的ObjectResult，根据Natty-Fetch要求的数据格式进行数据整理
    /// </summary>
    public class ErrorObjectResult : OkObjectResult
    {
        /// <summary>
        /// 实例化一个ErrorObjectResult
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        public ErrorObjectResult(string errorMessage)
            : base(null)
        {
            var msg = new ErrorResponseMessage(errorMessage);
            Value = msg;
        }

        /// <summary>
        /// 实例化一个ErrorObjectResult
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorLevel">错误级别</param>
        public ErrorObjectResult(int errorCode, string errorMessage, int errorLevel)
            : base(null)
        {
            var msg = new ErrorResponseMessage(errorCode, errorMessage, errorLevel);
            Value = msg;
        }
    }
}
