using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using SNAS.Core.Softs;

namespace SNAS.Core.SoftUsers
{
    /// <summary>
    /// 机器码换绑记录
    /// </summary>
    [Table("MCodeChangeLog")]
    public class MCodeChangeLog : CreationAuditedEntity<long>
    {

        [Column(Order = 0)]
        [ForeignKey("SoftUser")]
        [Required]
        public long SoftUserId { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        public virtual SoftUser SoftUser { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }

        /// <summary>
        /// 登录软件
        /// </summary>
        public virtual Soft Soft { get; set; }

        [ForeignKey("SoftUserLicense")]
        [Required]
        public long SoftUserLicenseId { get; set; }

        /// <summary>
        /// 登录卡密
        /// </summary>
        public virtual SoftUserLicense SoftUserLicense { get; set; }


        /// <summary>
        /// 授权类型
        /// </summary>
        public virtual MCodeChangeSource Source { get; set; }

        /// <summary>
        /// 原机器码
        /// </summary>
        public virtual string OldMCode { get; set; }

        /// <summary>
        /// 新机器码
        /// </summary>
        public virtual string NewMCode { get; set; }
    }
}
