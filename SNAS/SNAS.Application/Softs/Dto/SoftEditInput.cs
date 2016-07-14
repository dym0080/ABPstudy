using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftEditInput : IInputDto
    {
        public long Id { get; set; }

        [DisplayName("�������")]
        [Required]
        [StringLength(SNAS.Core.Softs.Soft.MaxNameLength)]
        public string Name { get; set; }

        [DisplayName("��ע")]
        [StringLength(SNAS.Core.Softs.Soft.MaxRemarkLength)]
        public string Remark { get; set; }

        public virtual SoftBindMode BindMode { get; set; }


        public virtual SoftRunMode RunMode { get; set; }

        public bool IsActive { get; set; }
    }
}