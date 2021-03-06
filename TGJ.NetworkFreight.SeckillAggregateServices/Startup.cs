
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Exceptions.Handlers;
using TGJ.NetworkFreight.Commons.Filters;
using TGJ.NetworkFreight.Commons.Users;
using TGJ.NetworkFreight.Cores.MicroClients.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using TGJ.NetworkFreight.Commons.MemoryCaches;
using Microsoft.AspNetCore.Http.Features;

namespace TGJ.NetworkFreight.SeckillAggregateServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            //5，将身份验证服务添加到DI并配置Bearer为默认方案。
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    //指定授权地址
                    options.Authority = Configuration["Authority"]; // 1、授权中心地址
                    options.RequireHttpsMetadata = false;
                    //获取或设置任何接收到的OpenIdConnect令牌的访问群体。
                    options.Audience = "TGJService";

                    ////设置验证时间时要应用的时钟偏移，即token多久验证一次，默认为5分钟
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(0);
                    ////指示令牌是否必须具有“过期”值
                    options.TokenValidationParameters.RequireExpirationTime = true;

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //       .AddIdentityServerAuthentication(options =>
            //       {
            //           options.Authority = Configuration["Authority"]; // 1、授权中心地址
            //           options.ApiName = "TGJService"; // 2、api名称(项目具体名称)
            //           options.RequireHttpsMetadata = false; // 3、https元数据，不需要
            //           options.JwtValidationClockSkew = TimeSpan.FromMinutes(0);
            //       });

            // 5、添加控制器
            services.AddControllers(options =>
            {
                options.Filters.Add<FrontResultWapper>(); // 1、通用结果
                options.Filters.Add<BizExceptionHandler>();// 2、通用异常
                options.ModelBinderProviders.Insert(0, new SysUserModelBinderProvider());// 3、自定义模型绑定
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).AddNewtonsoftJson(options =>
            {
                // 防止将大写转换成小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            // 6、使用内存缓存
            services.AddMemoryCache();

            services.AddScoped<ICaching, MemoryCaching>();

            //services.AddMemoryCacheSetup();


            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 5000; // 5000 items max
                options.ValueLengthLimit = 1024 * 1024 * 50; // 10MB max len form data
                options.KeyLengthLimit = 1024 * 1024 * 50;
            });
            #region 配置Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Demo", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"}
                           },new string[] { }
                        }
                    });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "TGJ.Api.xml");
                var xmlOrderPath = Path.Combine(basePath, "TGJ.NetworkFreight.Order.xml");
                var xmlUserPath = Path.Combine(basePath, "TGJ.NetworkFreight.User.xml");
                var xmlCertificationPath = Path.Combine(basePath, "TGJ.NetworkFreight.Certification.xml");

                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(xmlOrderPath);
                c.IncludeXmlComments(xmlUserPath);
                c.IncludeXmlComments(xmlCertificationPath);
            });

            #endregion 配置Swagger
        }


        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    var basePath = AppContext.BaseDirectory;
        //    var servicesDllFile = Path.Combine(basePath, "TGJ.NetworkFreight.SeckillAggregateServices.dll");

        //    var cacheType = new List<Type>();
        //    builder.RegisterType<CacheAOP>();
        //    cacheType.Add(typeof(CacheAOP));
        //    // 获取 Service.dll 程序集服务，并注册
        //    var assemblysServices = Assembly.LoadFrom(servicesDllFile);
        //    builder.RegisterAssemblyTypes(assemblysServices)
        //                .AsImplementedInterfaces()
        //                .InstancePerDependency()
        //                .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
        //                .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error!");
                    });
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // 1、开启身份认证
            app.UseAuthentication();

            app.UseAuthorization();
            // 2、使用跨域
            app.UseCors("AllowSpecificOrigin");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口文档");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}