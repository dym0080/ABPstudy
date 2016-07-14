using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class MCodeChangeLogCreateInput : IInputDto
    {
        [Required]
        public virtual string NewMCode { get; set; }

        public long SoftUserLicenseMcodeId { get; set; }

    }
}