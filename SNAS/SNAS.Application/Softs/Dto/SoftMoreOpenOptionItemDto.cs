using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftMoreOpenOptionItemDto
    {
        public long SoftId { get; set; }

        [Required]
        public virtual SoftMoreOpenRange MoreOpenRange { get; set; }

        /// <summary>
        /// 验证周期(分钟)
        /// </summary>
        public virtual int VerifyCycle { get; set; }

        /// <summary>
        /// 多开限制数量
        /// </summary>
        public virtual int LimitCount { get; set; }

    }
}
