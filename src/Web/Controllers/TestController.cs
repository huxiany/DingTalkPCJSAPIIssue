namespace EaseSource.AnDa.SMT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    public class TestController : SMTControllerBase
    {
        private readonly ILogger<TestController> logger;

        public TestController(ILogger<TestController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("")]
        [Route("Print")]
        public IActionResult Print()
        {
            return Success("成功导出数据");
        }
    }
}