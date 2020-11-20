using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using TGJ.NetworkFreight.Commons.Exceptions.Handlers;
using TGJ.NetworkFreight.Commons.Filters;
using TGJ.NetworkFreight.Commons.Users;
using TGJ.NetworkFreight.Cores.MicroClients.Extentions;
using TGJ.NetworkFreight.SeckillAggregateServices.MemoryCaches;

namespace TGJ.NetworkFreight.SeckillAggregateServices
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 1、注册服务发现
            object p = services.AddMicroClient(options =>
            {
                options.AssmelyName = "TGJ.NetworkFreight.SeckillAggregateServices";
                options.dynamicMiddlewareOptions = mo =>
                {
                    mo.serviceDiscoveryOptions = sdo =>
                    { sdo.DiscoveryAddress = Configuration["DiscoveryAddress"]; };
                };
            });

            services.AddScoped<ICaching, MemoryCaching>();

            /*// 1、服务发现
            services.AddServiceDiscovery(options => {
                options.DiscoveryAddress = "http://localhost:8500";
            });

            // 2、注册负载均衡
            services.AddLoadBalance();*/

            // 3、注册动态
            /* services.AddDynamicMiddleware<IDynamicMiddleService, DefaultDynamicMiddleService>(options => {
                 options.serviceDiscoveryOptions = options => { options.DiscoveryAddress = "http://localhost:8500"; };
             });*/

            // 3、设置跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                 builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });

            //4、添加身份认证
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = Configuration["Authority"]; // 1、授权中心地址
                        options.ApiName = "TGJService"; // 2、api名称(项目具体名称)
                        options.RequireHttpsMetadata = false; // 3、https元数据，不需要
                    });

            // 5、添加控制器
            services.AddControllers(options =>
            {
                options.Filters.Add<FrontResultWapper>(); // 1、通用结果
                options.Filters.Add<BizExceptionHandler>();// 2、通用异常
                options.ModelBinderProviders.Insert(0, new SysUserModelBinderProvider());// 3、自定义模型绑定
            }).AddNewtonsoftJson(options =>
            {
                // 防止将大写转换成小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            #region 配置Swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "聚合微服务文档", Version = "v1" });
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "TGJ.Api.xml");
                options.IncludeXmlComments($"{AppContext.BaseDirectory}/TGJ.Api.xml");
            });

            #endregion 配置Swagger
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/

            app.UseHttpsRedirection();

            app.UseRouting();

            // 1、开启身份认证
            app.UseAuthentication();

            app.UseAuthorization();
            // 2、使用跨域
            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口文档");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
