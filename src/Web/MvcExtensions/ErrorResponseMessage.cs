namespace EaseSource.AnDa.SMT.Web.MvcExtensions
{
    using Newtonsoft.Json;

    /// <summary>
    /// 调用失败的返回消息，按照Natty-Fetch的默认接收的数据格式专为其封装
    /// </summary>
    public class ErrorResponseMessage : ResponseMessage
    {
        /// <summary>
        /// 实例化一个ErrorResponseMessage
        /// </summary>
        /// <param name="errorMessage">要回传给调用者的错误详细信息</param>
        public ErrorResponseMessage(string errorMessage)
        {
            Success = false;
            Detail = new ErrorDetail { ErrMessage = errorMessage };
        }

        /// <summary>
        /// 实例化一个ErrorResponseMessage
        /// </summary>
        /// <param name="errorCode">要回传给调用者的错误代码</param>
        /// <param name="errorMessage">要回传给调用者的错误详细信息</param>
        /// <param name="errorLevel">要回传给调用者的错误级别</param>
        public ErrorResponseMessage(int errorCode, string errorMessage, int errorLevel)
        {
            Success = false;
            Detail = new ErrorDetail { ErrCode = errorCode, ErrMessage = errorMessage, ErrLevel = errorLevel };
        }

        /// <summary>
        /// 要回传给调用者的错误详细信息
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public ErrorDetail Detail { get; set; }
    }
}
