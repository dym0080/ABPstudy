using ABPDemoNoZero.Dictionarys;
using ABPDemoNoZero.People;
using System.Data.Entity.Migrations;

namespace ABPDemoNoZero.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ABPDemoNoZero.EntityFramework.ABPDemoNoZeroDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ABPDemoNoZero";
        }

        protected override void Seed(ABPDemoNoZero.EntityFramework.ABPDemoNoZeroDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...

            context.Dictionary.AddOrUpdate(
           p => p.Code,
           new Dictionary { Code = "00001001",Name="IPhone5s" },
           new Dictionary { Code = "00001002", Name = "IPhone6s" },
           new Dictionary { Code = "00001003", Name = "小米4" },
           new Dictionary { Code = "00001004", Name = "荣耀7" },
           new Dictionary { Code = "00001005", Name = "照相机" }
           );
        }
    }
}
