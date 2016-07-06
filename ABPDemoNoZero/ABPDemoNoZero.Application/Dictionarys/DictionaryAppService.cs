using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AutoMapper;
using ABPDemoNoZero.Dictionarys.Dtos;
using System;
using System.Collections.Generic;
using Abp.AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPDemoNoZero.Dictionarys
{
    public class DictionaryAppService: ApplicationService, IDictionaryAppService
    {
        private readonly IDictionaryRepository _dictionaryRepository;

        public DictionaryAppService(IDictionaryRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }

        public GetDictionarysOutput GetDictionarys()
        {
            //var dictionarys = _dictionaryRepository.GetAllDictionaryList();
            //var dictionaryDtos = dictionarys.MapTo<List<DictionaryDto>>();
            //return new PagedResultOutput<DictionaryDto>(dictionarys.Count, dictionaryDtos);

            //用AutoMapper自动将List<Task>转换成List<TaskDto>
            var dictionarys = _dictionaryRepository.GetAllDictionaryList();
            //var ss= dictionarys.MapTo<List<DictionaryDto>>();
            return new GetDictionarysOutput
            {

                Dictionarys = dictionarys//Mapper.Map<List<DictionaryDto>>(dictionarys)
            };
        }
    }
}
