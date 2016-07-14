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
    /// 软件卡密配置
    /// </summary>
    [Table("SoftLicenseOption")]
    public class SoftLicenseOption : AuditedEntity<long>
    {

        [ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }


        public virtual Soft Soft { get; set; }

        /// <summary>
        /// 卡密类型
        /// </summary>
        [Required]
        public virtual SoftLicenseType LicenseType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Required]
        public virtual decimal Price { get; set; }

        public virtual string LicenseTypeAlias
        {
            get
            {
                switch (LicenseType)
                {
                    case SoftLicenseType.Day:
                        return "日";
                    case SoftLicenseType.Hour:
                        return "小时";
                    case SoftLicenseType.Week:
                        return "周";
                    case SoftLicenseType.Month:
                        return "月";
                    case SoftLicenseType.Year:
                        return "年";
                    case SoftLicenseType.Forever:
                        return "永久";
                    default:
                        return "";
                }
            }
        }
    }
}
