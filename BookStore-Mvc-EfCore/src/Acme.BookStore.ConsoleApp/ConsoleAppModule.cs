using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace Acme.BookStore.ConsoleApp
{

    [DependsOn(
         typeof(AbpAutofacModule),
         typeof(AbpBackgroundWorkersQuartzModule)
     )]
    public class ConsoleAppModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.AddBackgroundWorker<MyLogWorker>();
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            PreConfigure<AbpQuartzOptions>(options =>
            {
                options.Configurator = configure =>
                {
                    configure.UsePersistentStore(storeOptions =>
                    {
                        storeOptions.UseProperties = true;
                        storeOptions.UseSqlServer(configuration.GetConnectionString("Default"));
                        storeOptions.UseClustering(t =>
                        {
                            t.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                            t.CheckinInterval = TimeSpan.FromSeconds(10);
                        });
                        storeOptions.UseJsonSerializer();
                    });
                };
            });

        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            Configure<AbpBackgroundWorkerQuartzOptions>(options =>
            {
                options.IsAutoRegisterEnabled = true;
            });

            //context.Services.AddHostedService<ConsoleAppHostedService>();
        }


    }
}
