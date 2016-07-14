using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SNAS.Application.MultiTenancy;
using SNAS.Core.MultiTenancy;

namespace SNAS.Application.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}