using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SNAS.Core.SoftUsers
{
    
    /// <summary>
    /// 软件用户
    /// </summary>
    [Table("SoftUser")]
    public class SoftUser : AuditedEntity<long>, IPassivable
    {
        /// <summary>
        /// 用户名称最大长度
        /// </summary>
        public const int MaxLoginNameLength = 50;

        /// <summary>
        /// 用密码最大长度
        /// </summary>
        public const int MaxPasswordLength = 128;

        /// <summary>
        ///QQ最大长度
        /// </summary>
        public const int MaxQQLength = 200;

        /// <summary>
        /// 手机号码最大长度
        /// </summary>
        public const int MaxMobileLength = 11;

        /// <summary>
        ///用户备注最大长度
        /// </summary>
        public const int MaxRemarkLength = 500;

        /// <summary>
        /// 用户名称
        /// </summary>
        [Required]
        [Index("IX_LoginName", 1, IsUnique = true)]
        [StringLength(MaxLoginNameLength)]
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required]
        [StringLength(MaxPasswordLength)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(MaxMobileLength)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        [StringLength(MaxQQLength)]
        public virtual string QQ { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public virtual SoftUserSource Source { get; set; }

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

    }
}
