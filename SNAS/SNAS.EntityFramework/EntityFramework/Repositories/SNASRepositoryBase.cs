using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace SNAS.EntityFramework.Repositories
{
    public abstract class SNASRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SNASDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected SNASRepositoryBase(IDbContextProvider<SNASDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class SNASRepositoryBase<TEntity> : SNASRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected SNASRepositoryBase(IDbContextProvider<SNASDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
