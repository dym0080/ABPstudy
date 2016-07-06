using Abp.EntityFramework;
using ABPDemoNoZero.Dictionarys;
using ABPDemoNoZero.People;
using ABPDemoNoZero.Tasks;
using System.Data.Entity;

namespace ABPDemoNoZero.EntityFramework
{
    public class ABPDemoNoZeroDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...
        public virtual IDbSet<Task> Tasks { get; set; }
        public virtual IDbSet<Person> People { get; set; }
        public virtual IDbSet<Dictionary> Dictionary { get; set; }

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ABPDemoNoZeroDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ABPDemoNoZeroDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ABPDemoNoZeroDbContext since ABP automatically handles it.
         */
        public ABPDemoNoZeroDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}
