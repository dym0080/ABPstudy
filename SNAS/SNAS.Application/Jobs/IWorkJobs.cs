using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using SNAS.Application.Softs.Dto;

namespace SNAS.Application.Jobs
{
    public interface IWorkJobs: ITransientDependency
    {
        /// <summary>
        /// kill掉长时间没有更新检测在线状态的在线记录
        /// </summary>
        /// <returns></returns>
        void KillTimeOutOnline();
    }
}
