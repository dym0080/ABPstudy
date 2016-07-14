using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SNAS.Core.SoftLicenses;

namespace SNAS.Core.SoftUsers
{
    /// <summary>
    /// 软件卡密绑定机器
    /// </summary>
    [Table("SoftUserLicenseMcode")]
    public class SoftUserLicenseMcode : CreationAuditedEntity<long>, IPassivable
    {
        [ForeignKey("SoftUserLicense")]
        [Index("IX_SoftUserLicense_Mcode", 1, IsUnique = true)]
        [Required]
        public long SoftUserLicenseId { get; set; }

        /// <summary>
        /// 登录卡密
        /// </summary>
        public virtual SoftUserLicense SoftUserLicense { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        [StringLength(32)]
        [Index("IX_SoftUserLicense_Mcode", 2, IsUnique = true)]
        public virtual string Mcode { get; set; }

     
        /// <summary>
        /// 是否有效
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

    }
}
