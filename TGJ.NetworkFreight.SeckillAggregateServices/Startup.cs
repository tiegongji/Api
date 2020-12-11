using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4.AccessTokenValidation;
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
using TGJ.NetworkFreight.SeckillAggregateServices.MemoryCaches;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

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
            // 1��ע�������
            object p = services.AddMicroClient(options =>
            {
                options.AssmelyName = "TGJ.NetworkFreight.SeckillAggregateServices";
                options.dynamicMiddlewareOptions = mo =>
                {
                    mo.serviceDiscoveryOptions = sdo =>
                    { sdo.DiscoveryAddress = Configuration["DiscoveryAddress"]; };
                };
            });

            /*// 1��������
            services.AddServiceDiscovery(options => {
                options.DiscoveryAddress = "http://localhost:8500";
            });

            // 2��ע�Ḻ�ؾ���
            services.AddLoadBalance();*/

            // 3��ע�ᶯ̬
            /* services.AddDynamicMiddleware<IDynamicMiddleService, DefaultDynamicMiddleService>(options => {
                 options.serviceDiscoveryOptions = options => { options.DiscoveryAddress = "http://localhost:8500"; };
             });*/

            // 3�����ÿ���
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                 builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });

            ////4����������֤
            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //        .AddIdentityServerAuthentication(options =>
            //        {
            //            options.Authority = Configuration["Authority"]; // 1����Ȩ���ĵ�ַ
            //            options.ApiName = "TGJService"; // 2��api����(��Ŀ��������)
            //            options.RequireHttpsMetadata = false; // 3��httpsԪ���ݣ�����Ҫ
            //            options.JwtValidationClockSkew = TimeSpan.FromSeconds(0);
            //             options.JwtBackChannelHandler

            //            options.Events = new JwtBearerEvents
            //            {
            //                OnAuthenticationFailed = context =>
            //                {
            //                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            //                    {
            //                        context.Response.Headers.Add("Token-Expired", "true");
            //                    }
            //                    return Task.CompletedTask;
            //                }

            //            };
            //        });

            ////�������֤������ӵ�DI������BearerΪĬ�Ϸ�����
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    //ָ����Ȩ��ַ
                    options.Authority = Configuration["Authority"]; // 1����Ȩ���ĵ�ַ
                    options.RequireHttpsMetadata = false;
                    //��ȡ�������κν��յ���OpenIdConnect���Ƶķ���Ⱥ�塣
                    options.Audience = "TGJService";

                    ////������֤ʱ��ʱҪӦ�õ�ʱ��ƫ�ƣ���token�����֤һ�Σ�Ĭ��Ϊ5����
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromMinutes(0);
                    ////ָʾ�����Ƿ������С����ڡ�ֵ
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
            //           options.Authority = Configuration["Authority"]; // 1����Ȩ���ĵ�ַ
            //           options.ApiName = "TGJService"; // 2��api����(��Ŀ��������)
            //           options.RequireHttpsMetadata = false; // 3��httpsԪ���ݣ�����Ҫ
            //           options.JwtValidationClockSkew = TimeSpan.FromMinutes(0);

            //       });

            // 5����ӿ�����
            services.AddControllers(options =>
            {
                options.Filters.Add<FrontResultWapper>(); // 1��ͨ�ý��
                options.Filters.Add<BizExceptionHandler>();// 2��ͨ���쳣
                options.ModelBinderProviders.Insert(0, new SysUserModelBinderProvider());// 3���Զ���ģ�Ͱ�
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }).AddNewtonsoftJson(options =>
            {
                // ��ֹ����дת����Сд
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddMemoryCacheSetup();

            #region ����Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Demo", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
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

            #endregion ����Swagger
        }

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

            // 1�����������֤
            app.UseAuthentication();

            app.UseAuthorization();
            // 2��ʹ�ÿ���
            app.UseCors("AllowSpecificOrigin");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "�ӿ��ĵ�");
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
