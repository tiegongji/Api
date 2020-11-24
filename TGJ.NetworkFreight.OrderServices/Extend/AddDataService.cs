using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Repositories;
using TGJ.NetworkFreight.OrderServices.Repositories.Impl;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using TGJ.NetworkFreight.OrderServices.Services;
using TGJ.NetworkFreight.OrderServices.Services.Impl;
using TGJ.NetworkFreight.OrderServices.Services.Interface;

namespace TGJ.NetworkFreight.OrderServices.Extend
{
    public static class AddDataService
    {
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="services">要扩展的类</param>
        /// <returns></returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            ////使用加载器技术，遍历所有的类，筛选实现IService接口的类，并过判断是否是类,并按照注解方式自动注入类
            //var a = AppDomain.CurrentDomain.GetAssemblies()
            //                                      //找到自己的类，WeButler是自己写的命名空间
            //                                      .FirstOrDefault(t => t.FullName.Contains("TGJ.NetworkFreight.OrderServices.Repositories") || t.FullName.Contains("TGJ.NetworkFreight.OrderServices.Services"))
            //                                      //获取所有对象
            //                                      .GetTypes()
            //                                      //判断是否是类
            //                                      .Where(a => a.IsClass)
            //                              //转换成list
            //                              .ToList();
            //AppDomain.CurrentDomain.GetAssemblies()
            //                                       //找到自己的类，WeButler是自己写的命名空间
            //                                       .FirstOrDefault(t => t.FullName.Contains("TGJ.NetworkFreight.OrderServices.Repositories") || t.FullName.Contains("TGJ.NetworkFreight.OrderServices.Services"))
            //                                      //获取所有对象
            //                                      .GetTypes()
            //                                      //判断是否是类
            //                                      .Where(a => a.IsClass)
            //                              //转换成list
            //                              .ToList()
            //                              //循环,并添加到di中
            //                              .ForEach(t =>
            //                              {
            //                                  services.AddScoped(t);
            //                              });
            services.AddScoped<IInitCategoryRepository, InitCategoryRepository>();
            services.AddScoped<IInitTruckRepository, InitTruckRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();
            services.AddScoped<IOrderFlowRepository, OrderFlowRepository>();
            services.AddScoped<IOrderReceiptImageRepository, OrderReceiptImageRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserAddressService, UserAddressService>();
            return services;
          
        }
    }
}
