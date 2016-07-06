using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPDemoNoZero.Dictionarys
{
    public interface IDictionaryRepository: IRepository<Dictionary>
    {
        List<Dictionary> GetAllDictionaryList();
    }
}
