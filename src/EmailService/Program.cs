using EmailService.Services.Logs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace EmailService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(cfg => cfg.AddProvider(new WebSocketLoggerProvider()))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(cfg => cfg
                        .AddJsonFile("appsettings.Secret.json", optional: true));
                    // webBuilder.ConfigureLogging(logging => logging.AddProvider(new WebSocketLoggerProvider()));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
