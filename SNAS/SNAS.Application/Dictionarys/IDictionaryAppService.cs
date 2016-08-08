using Abp.Application.Services;
using SNAS.Application.Dictionarys.Dto;
using SNAS.Application.Softs.Dto;
using SNAS.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Dictionarys
{
    public interface IDictionaryAppService: IApplicationService
    {
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AppPagedResultOutput<DictionaryListDto>> GetDictionayPageList(GetPageListInput input);

        /// <summary>
        /// 新增字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Create(DictionaryCreateInput input);

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Edit(DictionaryEditInput input);
    }
}
