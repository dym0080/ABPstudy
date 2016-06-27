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
            context.People.AddOrUpdate(
           p => p.Name,
           new Person { Name = "Isaac Asimov" },
           new Person { Name = "Thomas More" },
           new Person { Name = "George Orwell" },
           new Person { Name = "wwww paan" },
           new Person { Name = "Douglas Adams" }
           );
        }
    }
}
