using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SNAS.Application.SoftUsers.Dto;
using SNAS.Application.Utils;

namespace SNAS.Application.SoftUsers
{
    public interface ISoftUserAppService : IApplicationService
    {
        #region 用户

        Task Create(SoftUserCreateInput input);

        Task Edit(SoftUserEditInput input);

        Task<AppPagedResultOutput<SoftUserListDto>> GetPageList(GetPageListInput input);

        Task<SoftUserItemDto> Get(long id);

        Task Delete(long id);

        Task UseLicense(SoftUserUseLicenseInput input);

        Task ChangePassword(ChangePasswordDto dto);

        #endregion

        #region 用户授权

        Task<AppPagedResultOutput<SoftUserLicenseListDto>> GetLicensePageList(GetPageListInput input);

        Task EnableLicense(long id);

        Task DisableLicense(long id);

        Task<AppPagedResultOutput<SoftUserLicenseMcodeListDto>> GetLicenseMcodePageList(long softUserLicenseId, GetPageListInput input);

        Task EnableLicenseMcode(long id);

        Task DisableLicenseMcode(long id);

        Task DeleteLicenseMcode(long id);

        Task ChangeMcode(MCodeChangeLogCreateInput input);

        Task<AppPagedResultOutput<MCodeChangeLogListDto>> GetMCodeChangeLogPageList(long softUserLicenseId, GetPageListInput input);

        #endregion

        #region 登录记录

        Task<AppPagedResultOutput<SoftUserLoginListDto>> GetLoginPageList(GetPageListInput input); 
        
        #endregion
    }
}
