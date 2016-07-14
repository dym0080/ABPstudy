using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SNAS.Core.MultiTenancy;

namespace SNAS.Application.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantListDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}