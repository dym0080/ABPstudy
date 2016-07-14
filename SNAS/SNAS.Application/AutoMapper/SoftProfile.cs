using AutoMapper;
using SNAS.Core.Softs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SNAS.Application.Finances.Dto;
using SNAS.Application.Softs.Dto;
using SNAS.Application.SoftUsers.Dto;
using SNAS.Core.Finances;
using SNAS.Core.SoftLicenses;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.AutoMapper
{
    public class SoftProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.Initialize(cfg =>
            {

                #region 软件

                cfg.CreateMap<Soft, SoftCreateInput>().ReverseMap();
                cfg.CreateMap<Soft, SoftEditInput>().ReverseMap();
                cfg.CreateMap<Soft, SoftListDto>();
                cfg.CreateMap<Soft, SoftItemDto>();

                #endregion


                #region 软件价格

                cfg.CreateMap<SoftLicenseOption, SoftLicenseOptionCreateInput>().ReverseMap();
                cfg.CreateMap<SoftLicenseOption, SoftLicenseOptionEditInput>().ReverseMap();
                cfg.CreateMap<SoftLicenseOption, SoftLicenseOptionListDto>();
                cfg.CreateMap<SoftLicenseOption, SoftLicenseOptionItemDto>();
                cfg.CreateMap<SoftLicenseOption, ComboboxItemDto>()
                    .ForMember(dest => dest.DisplayText, opt => opt.MapFrom(src => src.LicenseTypeAlias))
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                    .ForMember(entity => entity.IsSelected, opt => opt.Ignore());
                cfg.CreateMap<SoftLicense, SoftLicenseListDto>();
                #endregion

                #region 软件配置

                cfg.CreateMap<SoftMoreOpenOption, SoftMoreOpenOptionItemDto>().ReverseMap();
                cfg.CreateMap<SoftRegisterOption, SoftRegisterOptionItemDto>().ReverseMap();
                cfg.CreateMap<SoftBindOption, SoftBindOptionItemDto>().ReverseMap();

                #endregion


                #region 软件用户

                cfg.CreateMap<SoftUser, SoftUserCreateInput>().ReverseMap();
                cfg.CreateMap<SoftUser, SoftUserEditInput>().ReverseMap();
                cfg.CreateMap<SoftUser, SoftUserListDto>();
                cfg.CreateMap<SoftUser, SoftUserItemDto>();
                cfg.CreateMap<SoftUserLicenseMcode, SoftUserLicenseMcodeListDto>();
                cfg.CreateMap<MCodeChangeLog, MCodeChangeLogListDto>();

                #endregion

                #region 财务资料

                cfg.CreateMap<Finance, FinanceListDto>(); 

                #endregion
            });
        }
    }
}
