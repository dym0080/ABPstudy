using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAS.Core.SoftLicenses;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Core.SoftLicenses
{
    /// <summary>
    /// 软件卡密
    /// </summary>
    [Table("SoftLicense")]
    public class SoftLicense : AuditedEntity<long>
    {
        /// <summary>
        ///备注最大长度
        /// </summary>
        public const int MaxRemarkLength = 500;

        [ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }

        /// <summary>
        /// 所属软件
        /// </summary>
        public virtual Soft Soft { get; set; }

        /// <summary>
        /// 卡密类型
        /// </summary>
        [Required]
        public virtual SoftLicenseType LicenseType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 卡密
        /// </summary>
        [Required]
        [StringLength(16)]
        [Index("IX_LicenseNo", 1, IsUnique = true)]
        public virtual string LicenseNo { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [Required]
        public virtual DateTime ApplyTime { get; set; }

        /// <summary>
        /// 出售时间
        /// </summary>
        public virtual DateTime? SellerTime { get; set; }

        /// <summary>
        /// 出售方式
        /// </summary>
        public virtual SoftLicenseSellType SellType { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }

        /// <summary>
        /// 卡密状态
        /// </summary>
        public virtual SoftLicenseStatus Status { get; set; }

        [ForeignKey("SoftUserId")]
        public virtual SoftUser SoftUser { get; set; }

        /// <summary>
        /// 使用的用户
        /// </summary>
        public virtual long? SoftUserId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaxRemarkLength)]
        public virtual string Remark { get; set; }
        
    }
}
