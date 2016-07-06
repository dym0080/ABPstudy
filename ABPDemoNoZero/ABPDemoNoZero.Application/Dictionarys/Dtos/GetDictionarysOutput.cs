using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPDemoNoZero.Dictionarys.Dtos
{
    public class GetDictionarysOutput: IOutputDto
    {
        public List<Dictionary> Dictionarys { get; set; }
    }
}
