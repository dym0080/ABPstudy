using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftLicenseOptionCreateInput : IInputDto
    {
        
        [Required]
        public long SoftId { get; set; }
        
        [Required]
        public decimal Price { get; set; }

        public virtual SoftLicenseType LicenseType { get; set; }
    }
}