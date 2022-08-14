using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Odev2.CompanyManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //// Read Configuration from appSettings
            //var config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //// Initialize Logger
            //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
            //Log.Information("Application is starting.");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSerilog()    // seri log
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
