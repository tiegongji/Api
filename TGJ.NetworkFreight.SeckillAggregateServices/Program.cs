using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TGJ.NetworkFreight.SeckillAggregateServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    //.UseUrls("https://localhost:5006")
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();

            //host.Run();

           // CreateHostBuilder(args).Build().Run();
           InitWebHost(args).Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //            //.UseKestrel(option =>
        //            //{
        //            //    option.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(8);
        //            //    option.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(8);
        //            //});
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           //.UseServiceProviderFactory(new AutofacServiceProviderFactory()) //<--NOTE THIS
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });

        public static IWebHost InitWebHost(string[] args)
        {
            var x509ca = new X509Certificate2(File.ReadAllBytes(@"tmsapi.51tgj.com.pfx"), "HD3P0YE9");
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(option =>
                {
                    option.ListenAnyIP(443, config => config.UseHttps(x509ca));
                    option.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(8);
                    option.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(8);
                })
                .Build();
        }
    }
}
