using System.Threading.Tasks;
using Abp.Application.Services;
using  SNAS.Application.Users.Dto;

namespace SNAS.Application.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);
    }
}