using AWS.Logger;
using AWS.Logger.SeriLog;
using IPFIN.Infrastructure.Postcode.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace IPFIN.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string allowSpecificOrigins = "_allowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            AWSLoggerConfig configuration = new AWSLoggerConfig("Serilog.DemoLogGroup");
            configuration.Region = Environment.GetEnvironmentVariable("AWS_REGION");

            var logger = new Serilog.LoggerConfiguration()
            .WriteTo.AWSSeriLog(configuration)
            .CreateLogger();

            services.AddSingleton<Serilog.ILogger>(logger);
            services.AddScoped<IPostcodesApiService, PostcodesApiService>();
            services.AddCors(options =>
            {
                options.AddPolicy(allowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://deploy-clientapp-s3.s3-website.ap-south-1.amazonaws.com")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(allowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}