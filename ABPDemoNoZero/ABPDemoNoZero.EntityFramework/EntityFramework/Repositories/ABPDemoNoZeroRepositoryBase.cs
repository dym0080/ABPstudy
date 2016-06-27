using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ABPDemoNoZero.EntityFramework.Repositories
{
    public abstract class ABPDemoNoZeroRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ABPDemoNoZeroDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ABPDemoNoZeroRepositoryBase(IDbContextProvider<ABPDemoNoZeroDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ABPDemoNoZeroRepositoryBase<TEntity> : ABPDemoNoZeroRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ABPDemoNoZeroRepositoryBase(IDbContextProvider<ABPDemoNoZeroDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
