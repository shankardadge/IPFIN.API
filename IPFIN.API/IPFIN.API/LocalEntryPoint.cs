using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace IPFIN.API
{
    /// <summary>
    /// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
    /// </summary>
    public class LocalEntryPoint
    {
        public static void Main(string[] args)
        {
            
            Environment.SetEnvironmentVariable("AWS_REGION", "ap-south-1");
            Environment.SetEnvironmentVariable("PostcodeServiceBaseUrl", "https://postcodes.io/postcodes");
            Environment.SetEnvironmentVariable("AutoCompleteServiceURL", "autocomplete");
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