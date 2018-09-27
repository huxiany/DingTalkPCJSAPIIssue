namespace EaseSource.AnDa.SMT.Web.MvcExtensions
{
    /// <summary>
    /// 要返回给Natty-Fetch的消息基类，按照Natty-Fetch的默认接收的数据格式专为其封装
    /// </summary>
    public abstract class ResponseMessage
    {
        /// <summary>
        /// 调用是否成功
        /// </summary>
        public bool Success { get; set; }
    }
}
