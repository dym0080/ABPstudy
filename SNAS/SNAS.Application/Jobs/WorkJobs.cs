using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Castle.Core.Logging;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.Jobs
{
    public class WorkJobs : IWorkJobs
    {
        private readonly IRepository<SoftOnlineLog, long> _softOnlineRepository;

        //2: 使用属性注入获得 logger
        public ILogger Logger { get; set; }
        
        public WorkJobs(IRepository<SoftOnlineLog, long> softOnlineRepository)
        {
            _softOnlineRepository = softOnlineRepository;

            //3: 如果没有提供Logger，就不能记录日志
            Logger = NullLogger.Instance;

        }


        [UnitOfWork]
        public void KillTimeOutOnline()
        {
            DateTime limitTime = DateTime.Now.AddMinutes(-5);

            var list = _softOnlineRepository.GetAll().Where(t => t.IsOnline && t.LastCheckTime<limitTime).ToList();//获取5分钟以上没有提交在线记录的数据
            foreach (var log in list)
            {
                log.IsOnline = false;
                log.OfflineReason = SoftOfflineReason.Kill;
                log.OfflineTime = DateTime.Now;
                _softOnlineRepository.Update(log);
            }

            Logger.Info(string.Format("共踢掉{0}条在线记录.", list.Count));
        }
    }
}
