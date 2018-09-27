namespace EaseSource.AnDa.SMT.Web.MvcExtensions
{
    using Newtonsoft.Json;

    /// <summary>
    /// 调用出错时返回的，根据Natty-Fetch要求的数据格式进行数据整理
    /// </summary>
    public class ErrorDetail
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int ErrCode { get; set; } = -1;

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string ErrMessage { get; set; } = string.Empty;

        /// <summary>
        /// 错误级别
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public int ErrLevel { get; set; } = 0;
    }
}
