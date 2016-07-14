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
    /// 软件绑定配置
    /// </summary>
    [Table("SoftBindOption")]
    public class SoftBindOption : AuditedEntity<long>
    {

        [Key, ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }

        public virtual Soft Soft { get; set; }

        /// <summary>
        /// 允许绑定数量
        /// </summary>
        public virtual int AllowBindCount { get; set; }

        /// <summary>
        /// 允许换绑次数
        /// </summary>
        public virtual int AllowChangeBindCount { get; set; }
    }
}
