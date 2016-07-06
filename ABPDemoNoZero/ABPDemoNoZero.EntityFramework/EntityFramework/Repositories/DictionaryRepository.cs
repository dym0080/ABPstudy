using Abp.EntityFramework;
using ABPDemoNoZero.Dictionarys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPDemoNoZero.EntityFramework.Repositories
{
    public class DictionaryRepository: ABPDemoNoZeroRepositoryBase<Dictionary>,IDictionaryRepository
    {
        protected DictionaryRepository(IDbContextProvider<ABPDemoNoZeroDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<Dictionary> GetAllDictionaryList()
        {
            var query = GetAll();
            return query.ToList();
        }
    }
}
