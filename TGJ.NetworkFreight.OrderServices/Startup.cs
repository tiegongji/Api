using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using TGJ.NetworkFreight.Commons.Exceptions.Handlers;
using TGJ.NetworkFreight.Commons.Filters;
using TGJ.NetworkFreight.Cores.Middleware.Extentions;
using TGJ.NetworkFreight.Cores.Registry.Extentions;

namespace TGJ.NetworkFreight.OrderServices
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        { 
            // ��ӷ���ע��
            services.AddServiceRegistry(options => {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "OrderServices";
                options.ServiceAddress = "https://localhost:5002";
                options.HealthCheckAddress = "/HealthCheck";

                options.RegistryAddress = "http://localhost:8500";
            });

            // ��ӿ�����
            services.AddControllers(options =>
            {
                options.Filters.Add<MiddlewareResultWapper>(1);
                options.Filters.Add<BizExceptionHandler>(2);
            }).AddNewtonsoftJson(option =>
            {
                // ��ֹ����дת����Сд
                option.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
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
            });
        }
    }
}
