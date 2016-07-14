using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftLicenseOptionEditInput : IInputDto
    {
        public long Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual SoftLicenseType LicenseType { get; set; }
    }
}