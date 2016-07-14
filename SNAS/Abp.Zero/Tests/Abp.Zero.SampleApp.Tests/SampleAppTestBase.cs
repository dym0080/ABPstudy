using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Collections;
using Abp.IdentityFramework;
using Abp.Modules;
using Abp.TestBase;
using Abp.Zero.SampleApp.EntityFramework;
using Abp.Zero.SampleApp.MultiTenancy;
using Abp.Zero.SampleApp.Roles;
using Abp.Zero.SampleApp.Tests.TestDatas;
using Abp.Zero.SampleApp.Users;
using Castle.MicroKernel.Registration;
using EntityFramework.DynamicFilters;
using Shouldly;

namespace Abp.Zero.SampleApp.Tests
{
    public abstract class SampleAppTestBase : AbpIntegratedTestBase
    {
        protected readonly RoleManager RoleManager;
        protected readonly UserManager UserManager;
        protected readonly IPermissionManager PermissionManager;
        protected readonly IPermissionChecker PermissionChecker;

        protected SampleAppTestBase()
        {
            CreateInitialData();

            RoleManager = Resolve<RoleManager>();
            UserManager = Resolve<UserManager>();
            PermissionManager = Resolve<IPermissionManager>();
            PermissionChecker = Resolve<IPermissionChecker>();
        }

        protected override void PreInitialize()
        {
            base.PreInitialize();

            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(Effort.DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );
        }

        private void CreateInitialData()
        {
            UsingDbContext(context => new InitialTestDataBuilder(context).Build());
        }

        protected override void AddModules(ITypeList<AbpModule> modules)
        {
            base.AddModules(modules);

            modules.Add<SampleAppEntityFrameworkModule>();
        }

        public void UsingDbContext(Action<AppDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<AppDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }

        public T UsingDbContext<T>(Func<AppDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<AppDbContext>())
            {
                context.DisableAllFilters();
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected Tenant GetDefaultTenant()
        {
            return UsingDbContext(
                context =>
                {
                    return context.Tenants.Single(t => t.TenancyName == Tenant.DefaultTenantName);
                });
        }

        protected User GetDefaultTenantAdmin()
        {
            var defaultTenant = GetDefaultTenant();
            return UsingDbContext(
                context =>
                {
                    return context.Users.Single(u => u.UserName == User.AdminUserName && u.TenantId == defaultTenant.Id);
                });
        }

        protected async Task<Role> CreateRole(string name)
        {
            return await CreateRole(name, name);
        }

        protected async Task<Role> CreateRole(string name, string displayName)
        {
            var role = new Role(null, name, displayName);

            (await RoleManager.CreateAsync(role)).Succeeded.ShouldBe(true);

            await UsingDbContext(async context =>
            {
                var createdRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == name);
                createdRole.ShouldNotBe(null);
            });

            return role;
        }

        protected async Task<User> CreateUser(string userName)
        {
            var user = new User
                       {
                           TenantId = AbpSession.TenantId,
                           UserName = userName,
                           Name = userName,
                           Surname = userName,
                           EmailAddress = userName + "@aspnetboilerplate.com",
                           IsEmailConfirmed = true,
                           Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                       };

            (await UserManager.CreateAsync(user)).CheckErrors();

            await UsingDbContext(async context =>
            {
                var createdUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                createdUser.ShouldNotBe(null);
            });

            return user;
        }

        protected async Task ProhibitPermissionAsync(Role role, string permissionName)
        {
            await RoleManager.ProhibitPermissionAsync(role, PermissionManager.GetPermission(permissionName));
            (await RoleManager.IsGrantedAsync(role.Id, PermissionManager.GetPermission(permissionName))).ShouldBe(false);
        }

        protected async Task GrantPermissionAsync(Role role, string permissionName)
        {
            await RoleManager.GrantPermissionAsync(role, PermissionManager.GetPermission(permissionName));
            (await RoleManager.IsGrantedAsync(role.Id, PermissionManager.GetPermission(permissionName))).ShouldBe(true);
        }

        protected async Task GrantPermissionAsync(User user, string permissionName)
        {
            await UserManager.GrantPermissionAsync(user, PermissionManager.GetPermission(permissionName));
            (await UserManager.IsGrantedAsync(user.Id, permissionName)).ShouldBe(true);
        }

        protected async Task ProhibitPermissionAsync(User user, string permissionName)
        {
            await UserManager.ProhibitPermissionAsync(user, PermissionManager.GetPermission(permissionName));
            (await UserManager.IsGrantedAsync(user.Id, permissionName)).ShouldBe(false);
        }
    }
}