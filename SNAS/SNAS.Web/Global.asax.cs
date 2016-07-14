using System;
using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;

namespace SNAS.Web
{
    public class MvcApplication : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            //HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();//注入EFProf.exe，监听工具，正式发不请去掉
            base.Application_Start(sender, e);
        }
    }
}
