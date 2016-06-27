using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace MyEventCloud.EntityFramework.Repositories
{
    public abstract class MyEventCloudRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MyEventCloudDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected MyEventCloudRepositoryBase(IDbContextProvider<MyEventCloudDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class MyEventCloudRepositoryBase<TEntity> : MyEventCloudRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MyEventCloudRepositoryBase(IDbContextProvider<MyEventCloudDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
