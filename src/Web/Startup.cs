namespace EaseSource.AnDa.SMT.Web.Utility
{
    using EaseSource.Dingtalk.Entity;
    using EaseSource.Dingtalk.Interfaces;
    using EaseSource.Dingtalk.Services;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add Cross Origin Resource Sharing
            services.AddCors(o => o.AddPolicy(
                "CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .WithMethods("POST", "GET")
                .AllowAnyHeader()
                .AllowCredentials()));

            services.AddOptions();
            services.Configure<DingtalkConfig>(Configuration.GetSection("DingtalkSettings"));

            // Add framework services.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });

            services.AddSingleton<IDingtalkServices, DingtalkServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
