using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LL.NET.Blog.Data.Repositories;
using Newtonsoft.Json.Serialization;
using LL.NET.Blog.Core.Services;
using LL.NET.Blog.Web.Services.Mail;

namespace LL.NET.Blog.Web
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json", false, true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_config["BlogDb:TestData"] == "True")
            {
                services.AddScoped<IBlogRepository, MemoryRepository>();
                services.AddScoped<IMailService, LoggingMailService>();
            }
            else
            {
                //services.AddScoped<IBlogRepository, DbRepository>();
                services.AddScoped<IMailService, SendGridMailService>();
            }
            
            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddJsonOptions(opts => opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                loggerFactory.AddDebug();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
