using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncDemo;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSample.Model;
using AutofacSample.Queue;
using AutofacSample.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestEaseSample;
using Serilog;

namespace AutofacSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope _container { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            QueueProvider<User>.AwsAccess = Configuration["AWS:Access"];
            QueueProvider<User>.AwsSecret = Configuration["AWS:Secret"];
            services.AddControllers();
            services.AddOptions();
            services.Configure<MyConfiguration>(Configuration.GetSection("myConfiguration"));
            services.AddHostedService<SQSPollService>();

        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            builder.RegisterType<GithubApi>().As<IGithubApi>();
            builder.RegisterType<BloggingContext>();
            builder.RegisterType<QueueProvider<User>>().As<IQueueProvider<User>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            _container = app.ApplicationServices.GetAutofacRoot();
            try
            {
                using(var scope = _container.BeginLifetimeScope())
                {
                    using (var serviceScope = scope.Resolve<BloggingContext>() as DbContext)
                    {
                        serviceScope.Database.Migrate();

                    }
                }
                
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Cannot migrate DB: ");
            }
        }
        
    }
}