using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SNAS.Application.Reports.Dto;

namespace SNAS.Application.Reports
{
    public interface IReportAppService : IApplicationService
    {
        Task<SystemStatistics> GetSystemStatistics();

        Task<List<DateCountInfo>> Get7DayIncome();

        Task<List<DateCountInfo>> Get7DayLicense();
    }
}
