using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using SNAS.Application.Reports.Dto;
using SNAS.Core.Finances;
using SNAS.Core.SoftLicenses;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;
using System.Data.Entity.Core.Objects;

namespace SNAS.Application.Reports
{
    public class ReportAppService : SNASAppServiceBase, IReportAppService
    {

        private readonly IRepository<Soft, long> _softRepository;
        private readonly IRepository<Finance, long> _financeRepository;
        private readonly IRepository<SoftUser, long> _softUserRepository;
        private readonly IRepository<SoftUserLicense, long> _softUserLicenseRepository;



        public ReportAppService(
            IRepository<Soft, long> softRepository,
            IRepository<Finance, long> financeRepository,
            IRepository<SoftUser, long> softUserRepository,
            IRepository<SoftUserLicense, long> softUserLicenseRepository
            )
        {
            _softRepository = softRepository;
            _financeRepository = financeRepository;
            _softUserRepository = softUserRepository;
            _softUserLicenseRepository = softUserLicenseRepository;
        }

        public virtual async Task<SystemStatistics> GetSystemStatistics()
        {
            SystemStatistics statistics = new SystemStatistics();
            statistics.Soft = await _softRepository.CountAsync();
            statistics.SoftUser = await _softUserRepository.CountAsync();
            var sum1 = (await _financeRepository.GetAll().Where(t => t.Type == FinanceType.InCome).SumAsync(t => (decimal?)t.Money)) ?? 0;
            var sum2 = await _financeRepository.GetAll().Where(t => t.Type == FinanceType.InCome && t.CreationTime.Day == DateTime.Now.Day).SumAsync(t => (decimal?)t.Money) ?? 0;

            statistics.TotalInCome = sum1;
            statistics.TodayInCome = sum2;

            return statistics;
        }

        public virtual async Task<List<DateCountInfo>> Get7DayIncome()
        {
            List<DateCountInfo> dateCountInfos = new List<DateCountInfo>();
            DateTime maxDate = DateTime.Now;
            DateTime minDate = DateTime.Now.Date.AddDays(-7);

            var orders = _financeRepository.GetAll()
                .Where(t => t.CreationTime >= minDate && t.CreationTime <= maxDate)
                // pull into memory here, since we won't be making the result set any smaller and
                // we want to use DateTime.ToString(), which EF won't compile to SQL
                .AsEnumerable()
                // this will group by the whole date. If you only want to group by part of the date,
                // (e. g. day or day, month), you could group by x => x.Date.Month or the like
                .GroupBy(x => x.CreationTime.Date)
                .OrderBy(x => x.Key)
                .Select(g => new
                {
                    Date = g.Select(c => c.CreationTime.ToString("MM-dd")).ToArray()[0],
                    Total = g.Sum(c => (c.Type == FinanceType.InCome ? c.Money : c.Money * -1))
                })
                .ToList();

            for (int i = 6; i >=0; i--)
            {
                string date = DateTime.Now.AddDays(-i).ToString("MM-dd");
                var item = orders.FirstOrDefault(t => t.Date == date);
                if(item==null)
                    dateCountInfos.Add(new DateCountInfo() {Date = date,Count = 0});
                else
                {
                    dateCountInfos.Add(new DateCountInfo() {Date = item.Date,Count = item.Total});
                }
            }

            return dateCountInfos;
        }

        public virtual async Task<List<DateCountInfo>> Get7DayLicense()
        {
            List<DateCountInfo> dateCountInfos = new List<DateCountInfo>();
            DateTime maxDate = DateTime.Now;
            DateTime minDate = DateTime.Now.Date.AddDays(-7);

            var orders = _softUserLicenseRepository.GetAll()
                .Where(t => t.CreationTime >= minDate && t.CreationTime <= maxDate)
                // pull into memory here, since we won't be making the result set any smaller and
                // we want to use DateTime.ToString(), which EF won't compile to SQL
                .AsEnumerable()
                // this will group by the whole date. If you only want to group by part of the date,
                // (e. g. day or day, month), you could group by x => x.Date.Month or the like
                .GroupBy(x => x.CreationTime.Date)
                .OrderBy(x => x.Key)
                .Select(g => new
                {
                    Date = g.Select(c => c.CreationTime.ToString("MM-dd")).ToArray()[0],
                    Total = g.Count()
                })
                .ToList();

            for (int i = 6; i >= 0; i--)
            {
                string date = DateTime.Now.AddDays(-i).ToString("MM-dd");
                var item = orders.FirstOrDefault(t => t.Date == date);
                if (item == null)
                    dateCountInfos.Add(new DateCountInfo() { Date = date, Count = 0 });
                else
                {
                    dateCountInfos.Add(new DateCountInfo() { Date = item.Date, Count = item.Total });
                }
            }

            return dateCountInfos;
        }
    }
}
