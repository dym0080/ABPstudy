using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;

namespace SNAS.Application.Jobs
{
   public class WorkJobsManager
    {

       public void KillTimeOutOnline()
       {
            ((IWorkJobs)IocManager.Instance.Resolve(typeof(IWorkJobs))).KillTimeOutOnline();
       }
    }
}
