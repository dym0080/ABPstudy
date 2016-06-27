using System.Linq;
using MyEventCloud.EntityFramework;
using MyEventCloud.MultiTenancy;

namespace MyEventCloud.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly MyEventCloudDbContext _context;

        public DefaultTenantCreator(MyEventCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
