namespace EaseSource.AnDa.SMT.Web.MvcExtensions
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 调用成功时返回的ObjectResult，根据Natty-Fetch要求的数据格式进行数据整理
    /// </summary>
    public class SuccessObjectResult : OkObjectResult
    {
        /// <summary>
        /// 实例化一个SuccessObjectResult
        /// </summary>
        /// <param name="value">ObjectResult要包含的对象，该对象会被序列化到Content属性中</param>
        public SuccessObjectResult(object value)
            : base(value)
        {
            var msg = new SuccessResponseMessage(value);
            Value = msg;
        }
    }
}
