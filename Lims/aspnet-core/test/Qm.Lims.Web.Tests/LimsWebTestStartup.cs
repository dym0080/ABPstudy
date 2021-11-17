using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Qm.Lims
{
    public class LimsWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<LimsWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}