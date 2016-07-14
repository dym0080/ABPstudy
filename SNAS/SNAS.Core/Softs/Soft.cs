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
    /// 软件
    /// </summary>
    [Table("Soft")]
    public class Soft : AuditedEntity<long>, IPassivable
    {
        /// <summary>
        /// 软件名称最大长度
        /// </summary>
        public const int MaxNameLength = 50;

        /// <summary>
        /// 软件备注最大长度
        /// </summary>
        public const int MaxRemarkLength = 500;

        /// <summary>
        /// 软件名称
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// appId
        /// </summary>
        [Required]
        [StringLength(32)]
        public virtual string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        [Required]
        [StringLength(32)]
        public virtual string AppSecret { get; set; }

        /// <summary>
        /// 软件绑定模式
        /// </summary>
        [Required]
        public virtual SoftBindMode BindMode { get; set; }

        /// <summary>
        /// 软件收费模式
        /// </summary>
        [Required]
        public virtual SoftRunMode RunMode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaxRemarkLength)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// 注册选项
        /// </summary>
        public virtual SoftRegisterOption SoftRegisterOption { get; set; }

        /// <summary>
        /// 多开选项
        /// </summary>
        public virtual SoftMoreOpenOption SoftMoreOpenOption { get; set; }

        /// <summary>
        /// 绑定配置
        /// </summary>
        public virtual SoftBindOption SoftBindOption { get; set; }


    }
}
