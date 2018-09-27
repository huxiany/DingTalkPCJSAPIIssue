namespace EaseSource.AnDa.SMT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EaseSource.Dingtalk.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 钉钉API
    /// </summary>
    [Route("api/[controller]")]
    public class DingtalkController : SMTControllerBase
    {
        private readonly IDingtalkServices ddSVC;

        public DingtalkController(IDingtalkServices dingtalkSVC)
        {
            ddSVC = dingtalkSVC;
        }

        /// <summary>
        /// 获取钉钉配置信息，用于PC端阿里钉钉初始化
        /// </summary>
        /// <returns>钉钉配置信息</returns>
        [HttpGet]
        [Route("GetPCConfig")]
        public async Task<IActionResult> GetPCConfig(string clientUrl)
        {
            if (string.IsNullOrEmpty(clientUrl))
            {
                return BadRequest("clientUrl为空");
            }

            return Success(await ddSVC.GetPCConfigAsync(clientUrl));
        }
    }
}
