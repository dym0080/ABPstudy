using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftLicenseOptionItemDto
    {
        public long Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual SoftLicenseType LicenseType { get; set; }

    }
}
