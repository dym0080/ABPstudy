using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using SNAS.Application.Finances.Dto;
using SNAS.Application.Softs.Dto;
using SNAS.Application.Utils;
using SNAS.Core.Finances;
using SNAS.Core.Softs;

namespace SNAS.Application.Finances
{
    public class FinanceAppService : SNASAppServiceBase, IFinanceAppService
    {

        private readonly IRepository<Finance, long> _financeRepository;

        public FinanceAppService(
            IRepository<Finance, long> financeRepository)
        {
            _financeRepository = financeRepository;
        }


        public virtual async Task<AppPagedResultOutput<FinanceListDto>> GetPageList(GetPageListInput input)
        {
            var query = _financeRepository.GetAll();
            var where = FilterExpression.FindByGroup<Finance>(input.Filter);
            var count = await query.Where(where).CountAsync();
            var list = await query.Where(where)
                .OrderByDescending(t => t.CreationTime)
                .PageBy(input)
                .ToListAsync();

            var pageList = list.MapTo<List<FinanceListDto>>();
            return new AppPagedResultOutput<FinanceListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }
    }
}
