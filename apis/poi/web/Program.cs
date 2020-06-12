using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using poi.Utility;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace poi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
       public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

           static void Demp1()
        {
            throw new System.Exception();
        }

        static void Demo3()
        {
            var abc = "Server = tcp:openhackn82382w0sql.database.windows.net,1433; Initial Catalog = mydrivingDB; Persist Security Info = False; User ID = demousersa1; Password = demo@pass123; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30; ";
        }
    
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //used to read env variables for host/port
            var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseConfiguration(configuration)
                    .UseIISIntegration()
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                        logging.AddDebug();
                    })
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                        config.AddCommandLine(args);
                    })
                    .UseStartup<Startup>()
                    .UseUrls(POIConfiguration.GetUri(configuration));
                });

            return host;
        }
    }
}
