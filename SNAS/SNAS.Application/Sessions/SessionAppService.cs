using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.UI;
using Microsoft.AspNet.Identity;
using SNAS.Application.Sessions.Dto;

namespace SNAS.Application.Sessions
{
    [AbpAuthorize]
    public class SessionAppService : SNASAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>()
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }

            return output;
        }

        public async Task ChangePassword(ChangePasswordDto changePassworDto)
        {
            if (!this.AbpSession.UserId.HasValue)
            {
                throw new UserFriendlyException("请登录后再修改密码!");
            }

            if (changePassworDto.NewPassword.Length < 6)
            {
                throw new UserFriendlyException("新密码长度不能少于6位!");
            }

            if (changePassworDto.NewPassword != changePassworDto.NewPassword2)
            {
                throw new UserFriendlyException("2次输入的新密码不一致!");
            }

            var user = await UserManager.GetUserByIdAsync(this.AbpSession.UserId.Value);
            var verificationResult = new PasswordHasher().VerifyHashedPassword(user.Password, changePassworDto.OldPassword);
            if (verificationResult != PasswordVerificationResult.Success)
            {
                throw new UserFriendlyException("旧密码不正确!");
            }

            await UserManager.ChangePasswordAsync(user, changePassworDto.NewPassword);
        }
    }
}