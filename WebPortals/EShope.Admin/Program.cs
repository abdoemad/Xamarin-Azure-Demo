using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EShope.Admin
{
    public class Program
    {
        public static string SubscriptionName = "pendingorders-handler";
        public static void Main(string[] args)
        {
            Console.WriteLine(" ooooooooooooooo " + (args==null).ToString() + ( args != null ? args.Length > 0 ? " *"+string.Join('-', args) : " *empty" : " *null args"));
            var port = args != null ? args.Length > 0 ? args[0] : "6001": "6001";
            SubscriptionName = args != null ? args.Length > 1 ? args[1] : "pendingorders-handler": "pendingorders-handler";
            
            Console.WriteLine("oooooo");

            CreateWebHostBuilder(args)
                .UseUrls($"http://localhost:{ port }/")
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
