using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SNAS.Application.Finances.Dto;
using SNAS.Application.Utils;

namespace SNAS.Application.Finances
{
   public interface IFinanceAppService : IApplicationService
    {
        Task<AppPagedResultOutput<FinanceListDto>> GetPageList(GetPageListInput input);
    }
}
