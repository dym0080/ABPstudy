using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.SoftLicenses;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftLicenseListDto
    {
        public virtual long Id { get; set; }

        public virtual decimal Price { get; set; }

        public virtual SoftLicenseType LicenseType { get; set; }

        public DateTime CreationTime { get; set; }


        public string LicenseTypeAlias
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

        /// <summary>
        /// 卡密
        /// </summary>
        [Required]
        [StringLength(16)]
        public virtual string LicenseNo { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [Required]
        public virtual DateTime ApplyTime { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public virtual DateTime? UseTime { get; set; }

        /// <summary>
        /// 卡密状态
        /// </summary>
        public virtual SoftLicenseStatus Status { get; set; }

    
        public string StatusAlias
        {
            get
            {
                switch (Status)
                {
                    case SoftLicenseStatus.Normal:
                        return "正常";
                    case SoftLicenseStatus.Sell:
                        return "已售出";
                    case SoftLicenseStatus.HasUse:
                        return "已使用";
                    case SoftLicenseStatus.Retuurn:
                        return "退货";
                    default:
                        return "";
                }
            }
        }

        public string SoftUser_LoginName { get; set; }
    }
}
