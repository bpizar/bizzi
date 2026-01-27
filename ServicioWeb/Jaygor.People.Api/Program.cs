using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Jaygor.People.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;

//namespace Jaygor.People.Api
//{
//    public class Program
//    {
//        private static IConfiguration configuration { get; }
//        private static string uri;

//        static Program()
//        {
//            var configurationBuilder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json");
//            configuration = configurationBuilder.Build();
//        }

//        public static void Main(string[] args)
//        {
//            try
//            {
//                // BuildWebHost(args).Run();
//                string appRootPath = ".";
//                uri = configuration["serverUri"];

//                BuildWebHost(appRootPath, args).Run();
//            }
//            catch(Exception ex)
//            {
//                Console.Write(ex.Message);
//            }
//        }

//        public static IWebHost BuildWebHost(string appRootPath, string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//            .UseKestrel()
//            .UseUrls(urls: uri)
//                .UseContentRoot(appRootPath)
//                .ConfigureAppConfiguration((hostingContext, config) =>
//                {
//                    config.AddJsonFile(string.Format("{0}/appsettings.json", appRootPath));
//                })
//                .UseStartup<Startup>()
//                .Build();


//        public static IWebHostBuilder GetWebHostBuilder(string appRootPath, string[] args)
//        {
//            var webHostBuilder = new WebHostBuilder()
//                .UseKestrel()
//                .UseContentRoot(appRootPath)
//                .ConfigureAppConfiguration((hostingContext, config) =>
//                {
//                    config.AddJsonFile(string.Format("{0}/appsettings.json", appRootPath));
//                })
//                .UseStartup<Startup>();

//            return webHostBuilder;
//        }

//        //public static IWebHost BuildWebHost(string[] args) =>
//        //WebHost.CreateDefaultBuilder(args)
//        //.UseStartup<Startup>()
//        //.UseKestrel()
//        //.Build();

//        //si no da luego quitar usekestrel
//    }
//}