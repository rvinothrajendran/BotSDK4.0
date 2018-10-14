using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotStateSample
{
    public class Startup
    {
        private IHostingEnvironment _environment;
        public Startup(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var buildConfig = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var config = buildConfig.Build();
            services.AddSingleton(config);

            services.AddBot<BotStateDemo>(serviceBot =>
            {
                serviceBot.CredentialProvider = new ConfigurationCredentialProvider(config);
            });

            services.AddSingleton<StateHelper>(st =>
            {
                var stateHelp = new StateHelper();
                return stateHelp;

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseBotFramework();
        }
    }
}
