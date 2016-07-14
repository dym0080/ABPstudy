using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Core.Softs
{
    /// <summary>
    /// 软件注册配置
    /// </summary>
    [Table("SoftRegisterOption")]
    public class SoftRegisterOption: AuditedEntity<long>
    {

        [Key, ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }


        public virtual Soft Soft { get; set; }

        /// <summary>
        /// 是否允许注册
        /// </summary>
        public virtual bool AllowRegister { get; set; }
        

        /// <summary>
        /// 试用时间（分钟）
        /// </summary>
        public virtual int TrialTime { get; set; }
    }
}
