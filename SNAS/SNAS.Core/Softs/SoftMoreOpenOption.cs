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
    /// 软件多开配置
    /// </summary>
    [Table("SoftMoreOpenOption")]
    public class SoftMoreOpenOption : AuditedEntity<long>
    {

        [Key, ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }


        public virtual Soft Soft { get; set; }

        /// <summary>
        /// 多开范围
        /// </summary>
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
