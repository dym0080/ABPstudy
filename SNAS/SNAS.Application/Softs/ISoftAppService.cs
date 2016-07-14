using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SNAS.Application.Softs.Dto;
using SNAS.Application.Utils;

namespace SNAS.Application.Softs
{
    public interface ISoftAppService : IApplicationService
    {
        #region 软件

        Task Create(SoftCreateInput input);

        Task Edit(SoftEditInput input);

        Task<AppPagedResultOutput<SoftListDto>> GetPageList(GetPageListInput input);

        Task<SoftItemDto> Get(long id);

        Task Delete(long id);

        #endregion

        #region 软件价格

        Task CreateLicenseOption(SoftLicenseOptionCreateInput input);

        Task EditLicenseOption(SoftLicenseOptionEditInput input);

        Task<ListResultDto<SoftLicenseOptionListDto>> GetLicenseOptionPageList(long softId);

        Task<SoftLicenseOptionItemDto> GetLicenseOption(long id);

        Task DeleteLicenseOption(long id);

        #endregion

        #region 多开配置

        Task<SoftMoreOpenOptionItemDto> GetMoreOpenOption(long softId);
        Task SaveMoreOpenOption(SoftMoreOpenOptionItemDto input);

        #endregion

        #region 注册配置

        Task<SoftRegisterOptionItemDto> GetRegisterOption(long softId);

        Task SaveRegisterOption(SoftRegisterOptionItemDto input);

        #endregion

        #region 绑定配置

        Task<SoftBindOptionItemDto> GetBindOption(long softId);

        Task SaveBindOption(SoftBindOptionItemDto input);

        #endregion


        #region 卡密

        Task<ListResultDto<ComboboxItemDto>> GetLicenseOptionComboList(long softId);

        Task GenerateLicenses(SoftLicenseGenerateItemInput input);

        Task<AppPagedResultOutput<SoftLicenseListDto>> GetLicensePageList(long softId, GetPageListInput input);

        Task DeleteLicense(long id);

        Task SellLicense(long id);

        Task ReturnLicense(long id);

        #endregion
    }

}
