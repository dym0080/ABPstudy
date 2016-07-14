using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Application.Users;
using SNAS.Core.MultiTenancy;
using SNAS.Core.Users;

namespace SNAS.Application.MultiTenancy.Dto
{
    public class CreateTenantInput : IInputDto
    {
        [Required]
        [StringLength(Tenant.MaxTenancyNameLength)]
        [RegularExpression(Tenant.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(Tenant.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }
    }
}