using SNAS.EntityFramework;
using EntityFramework.DynamicFilters;

namespace SNAS.Migrations.SeedData
{
    public class InitialDataBuilder
    {
        private readonly SNASDbContext _context;

        public InitialDataBuilder(SNASDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            _context.DisableAllFilters();

            new DefaultEditionsBuilder(_context).Build();
            new DefaultTenantRoleAndUserBuilder(_context).Build();
            new DefaultLanguagesBuilder(_context).Build();


        }
    }
}
