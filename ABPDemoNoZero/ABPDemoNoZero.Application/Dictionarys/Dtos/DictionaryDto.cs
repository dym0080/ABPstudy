using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPDemoNoZero.Dictionarys.Dtos
{
    public class DictionaryDto: EntityDto
    {
        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public override string ToString()
        {
            return "DictionaryDtoToString";
        }
    }
}
