using MyEventCloud.EntityFramework;
using EntityFramework.DynamicFilters;

namespace MyEventCloud.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly MyEventCloudDbContext _context;

        public InitialHostDbBuilder(MyEventCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
