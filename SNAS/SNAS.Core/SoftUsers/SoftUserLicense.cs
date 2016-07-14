using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SNAS.Core.Softs;

namespace SNAS.Core.SoftUsers
{
    /// <summary>
    /// 用户登录记录
    /// </summary>
    [Table("SoftUserLicense")]
    public class SoftUserLicense : CreationAuditedEntity<long>, IPassivable
    {

        /// <summary>
        ///备注最大长度
        /// </summary>
        public const int MaxRemarkLength = 500;


        /// <summary>
        /// Ip最大长度
        /// </summary>
        public const int MaxIpLength = 15;

        [Column(Order = 0)]
        [ForeignKey("SoftUser")]
        [Index("IX_SoftUserId_SoftId", 2, IsUnique = true)]
        [Required]
        public long SoftUserId { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public virtual SoftUser SoftUser { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Soft")]
        [Index("IX_SoftUserId_SoftId", 1, IsUnique = true)]
        [Required]
        public long SoftId { get; set; }

        /// <summary>
        /// 登录软件
        /// </summary>
        public virtual Soft Soft { get; set; }

        /// <summary>
        /// 授权类型
        /// </summary>
        public virtual SoftUserLicenseType Type { get; set; }

        /// <summary>
        /// 授权时间
        /// </summary>
        public virtual DateTime AuthorizeTime { get; set; }

        /// <summary>
        /// 到期时间(为空时，授权为永久)
        /// </summary>
        public virtual DateTime? ExpireTime { get; set; }

        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最近一次登录ip
        /// </summary>
        [StringLength(MaxIpLength)]
        public virtual string LastLoginIp { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Required]
        public bool IsActive { get; set; }


        [StringLength(MaxRemarkLength)]
        public virtual string Remark { get; set; }

    }
}
