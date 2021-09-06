using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Profile.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //WebHost.CreateDefaultBuilder(args)
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.AddServerHeader = false;
                        options.ConfigureHttpsDefaults(httpsConnectionAdapterOptions =>
                        {
                            httpsConnectionAdapterOptions.ClientCertificateValidation =
                                (certificate2, chain, arg3) => true;

                        });
                    });

                }).UseNLog();
    }
}
