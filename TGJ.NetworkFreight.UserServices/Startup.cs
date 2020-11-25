using System;
using System.Linq;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using TGJ.NetworkFreight.Commons.Exceptions.Handlers;
using TGJ.NetworkFreight.Commons.Filters;
using TGJ.NetworkFreight.Cores.Registry.Extentions;
using TGJ.NetworkFreight.UserServices.Configs;
using TGJ.NetworkFreight.UserServices.Context;
using TGJ.NetworkFreight.UserServices.IdentityServer;
using TGJ.NetworkFreight.UserServices.Repositories;
using TGJ.NetworkFreight.UserServices.Services;

namespace TGJ.NetworkFreight.UserServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // 1、IOC容器中注入dbcontext
            services.AddDbContext<UserContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // 注册用户service
            services.AddScoped<IUserService, UserService>();

            // 注册用户仓储
            services.AddScoped<IUserRepository, UserRepository>();

            // 注册用户地址service
            services.AddScoped<IUserAddressService, UserAddressService>();

            // 注册用户地址仓储
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();

            //添加服务注册
            services.AddServiceRegistry(options =>
            {
                options.ServiceId = Guid.NewGuid().ToString();
                options.ServiceName = "UserServices";
                options.ServiceAddress = Configuration["ServiceAddress"];
                options.HealthCheckAddress = "/health";

                options.RegistryAddress = Configuration["RegistryAddress"];
            });

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // 添加IdentityServer4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(Configuration.GetConnectionString("IdsConnection"),
                                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();// 2、自定义用户校验


            // 1、ioc容器中添加IdentityServer4
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //   .AddInMemoryApiResources(Config.GetApiResources())
            //    .AddInMemoryClients(Config.GetClients())
            //    .AddTestUsers(Config.GetUsers());

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddInMemoryApiResources(Config.GetApiResources())
            //    .AddInMemoryClients(Config.GetClients())
            //    .AddInMemoryIdentityResources(Config.Ids)
            //    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();


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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // 2、使用IdentityServer
            app.UseIdentityServer();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            InitializeDatabase(app);
        }

        // 将config中数据存储起来
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.Ids)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetResource())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
