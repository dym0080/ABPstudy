using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using SNAS.Application.MultiTenancy;
using SNAS.Application.Users;
using Microsoft.AspNet.Identity;
using SNAS.Core.MultiTenancy;
using SNAS.Core.Users;

namespace SNAS.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class SNASAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected SNASAppServiceBase()
        {
            LocalizationSourceName = SNASConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}