using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Acme.BookStore.ConsoleApp
{
    public class MyLogWorker : QuartzBackgroundWorkerBase
    {
        private readonly HelloWorldService _helloWorldService;

        public MyLogWorker(HelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
            JobDetail = JobBuilder.Create<MyLogWorker>().WithIdentity(nameof(MyLogWorker)).Build();

            Trigger = TriggerBuilder.Create()
                .WithIdentity(nameof(MyLogWorker)).WithSimpleSchedule(
                s => s.WithIntervalInMinutes(1).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
                .Build();

            ScheduleJob = async scheduler =>
            {
                if (!await scheduler.CheckExists(JobDetail.Key))
                {
                    await scheduler.ScheduleJob(JobDetail, Trigger);
                }
            };
        }

        public override Task Execute(IJobExecutionContext context)
        {
            _helloWorldService.SayHello();
            Logger.LogInformation("Executed MyLogWorker..!");
            return Task.CompletedTask;
        }
    }
}
