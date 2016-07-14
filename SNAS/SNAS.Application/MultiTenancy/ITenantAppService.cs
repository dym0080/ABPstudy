using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using  SNAS.Application.MultiTenancy.Dto;

namespace SNAS.Application.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultOutput<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
