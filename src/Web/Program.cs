namespace EaseSource.AnDa.SMT.Web.Utility
{
    using System;
    using System.IO;
    using EaseSource.Dingtalk.Interfaces;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    public class Program
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .ConfigureLogging(ConfigureLogger)
#if DEBUG
                .UseUrls("http://*:5000")
#endif
                .UseStartup<Startup>();
        }

        private static void ConfigureLogger(WebHostBuilderContext ctx, ILoggingBuilder logging)
        {
            logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
        }
    }
}
