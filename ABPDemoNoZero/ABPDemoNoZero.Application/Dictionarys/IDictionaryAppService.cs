using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ABPDemoNoZero.Dictionarys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPDemoNoZero.Dictionarys
{
    public interface IDictionaryAppService: IApplicationService
    {
        GetDictionarysOutput GetDictionarys();
    }
}
