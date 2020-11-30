using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using TGJ.NetworkFreight.CertificationServices.Services;
using TGJ.NetworkFreight.Commons.Exceptions.Handlers;
using TGJ.NetworkFreight.Commons.Filters;
using TGJ.NetworkFreight.Cores.Registry.Extentions;

namespace TGJ.NetworkFreight.CertificationServices
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加服务注册

            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "CertificationServices";
                options.ServiceAddress = Configuration["ServiceAddress"];
                options.HealthCheckAddress = "/health";

                options.RegistryAddress = Configuration["RegistryAddress"];

                //services.AddServiceRegistry(options =>
                //{
                //    options.ServiceId = Guid.NewGuid().ToString();
                //    options.ServiceName = "CertificationServices";
                //    options.ServiceAddress = "https://localhost:5003";
                //    options.HealthCheckAddress = "/HealthCheck";
                //    options.RegistryAddress = "http://localhost:8500";
                //});
            });

            // 认证service
            services.AddScoped<ICertificationService, CertificationService>();

            // 添加控制器
            services.AddControllers(options =>
            {
                options.Filters.Add<MiddlewareResultWapper>(1);
                options.Filters.Add<BizExceptionHandler>(2);
            }).AddNewtonsoftJson(option =>
            {
                // 防止将大写转换成小写
                option.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddHealthChecks();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
