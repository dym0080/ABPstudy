using System.Threading.Tasks;
using Abp.Application.Services;
using  SNAS.Application.Roles.Dto;

namespace SNAS.Application.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
