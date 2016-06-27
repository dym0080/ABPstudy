using System.Threading.Tasks;
using Abp.Application.Services;
using MyEventCloud.Roles.Dto;

namespace MyEventCloud.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
