using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftRegisterOptionItemDto
    {
        public long SoftId { get; set; }

        public virtual bool AllowRegister { get; set; }
        
        /// <summary>
        /// 试用时间（分钟）
        /// </summary>
        public virtual int TrialTime { get; set; }

    }
}
