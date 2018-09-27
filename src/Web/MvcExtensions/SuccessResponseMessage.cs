namespace EaseSource.AnDa.SMT.Web.MvcExtensions
{
    /// <summary>
    /// 调用成功的返回消息，按照Natty-Fetch的默认接收的数据格式专为其封装
    /// </summary>
    public class SuccessResponseMessage : ResponseMessage
    {
        /// <summary>
        /// 实例化一个SuccessResponseMessage
        /// </summary>
        /// <param name="content">要回传给调用者的内容</param>
        public SuccessResponseMessage(object content)
        {
            Success = true;
            this.Content = content;
        }

        /// <summary>
        /// 要回传给调用者的内容
        /// </summary>
        public object Content { get; set; }
    }
}
