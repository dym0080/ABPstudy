using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftCreateInput : IInputDto
    {
        [DisplayName("Èí¼þÃû³Æ")]
        [Required]
        [StringLength(SNAS.Core.Softs.Soft.MaxNameLength)]
        public string Name { get; set; }

        [DisplayName("±¸×¢")]
        [StringLength(SNAS.Core.Softs.Soft.MaxRemarkLength)]
        public string Remark { get; set; }

        public virtual SoftBindMode BindMode { get; set; }


        public virtual SoftRunMode RunMode { get; set; }
    }
}