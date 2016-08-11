using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using SNAS.Application.Dictionarys.Dto;
using SNAS.Application.Utils;
using SNAS.Core.Dictionarys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Dictionarys
{
    public class DictionaryAppService: SNASAppServiceBase,IDictionaryAppService
    {
        private readonly IRepository<Dictionary,long> _dicRepository;

        public DictionaryAppService(IRepository<Dictionary,long> dicRepository)
        {
            _dicRepository = dicRepository;
        }

        public virtual async Task<AppPagedResultOutput<DictionaryListDto>> GetDictionayPageList(GetPageListInput input)
        {
            var query = _dicRepository.GetAll();
            var where = FilterExpression.FindByGroup<Dictionary>(input.Filter);
            var count = await query.Where(where).CountAsync();
            var list = await query.Where(where)
                .OrderByDescending(t => t.CreationTime)
                .PageBy(input)
                .ToListAsync();

            var pageList = list.MapTo<List<DictionaryListDto>>();
            return new AppPagedResultOutput<DictionaryListDto>(count, pageList, input.CurrentPage, input.PageSize);
        }

        public virtual async Task Create(DictionaryCreateInput input)
        {
            var item = input.MapTo<Dictionary>();
            var entity = await _dicRepository.InsertAsync(item);
        }

        public virtual async Task Edit(DictionaryEditInput input)
        {
            var entity = await _dicRepository.GetAsync(input.Id);
            entity.Name = input.Name;
            entity.Type = input.Type;
            entity.Value = input.Value;
            entity.Description = input.Description;
            entity.OrderId = input.OrderId;
            entity.ParentId = input.ParentId;
            entity.IsDeleted = input.IsDeleted;
            await _dicRepository.UpdateAsync(entity);
        }
    }
}
