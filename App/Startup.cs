using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AirportsDemo.App.Services;
using AirportsDemo.App.Services.Impl;
using System.Net.Http;
using Polly.Extensions.Http;
using Polly;

namespace AirportsDemo.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddHttpClient<IFlightsApiClient, FlightsApiClient>(client => {
                client.BaseAddress = new Uri("https://homework.appulate.com");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler() { MaxConnectionsPerServer = 4 })
            .AddPolicyHandler(
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(3, retryAttemt => TimeSpan.FromSeconds(1))
            );

            services.AddScoped<IAirlinesCache, AirlinesCache>();
            services.AddScoped<IFlightsService, FlightsService>();
            services.AddScoped<IRouteFinder, RouteFinder>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseMvc();
        }
    }
}
