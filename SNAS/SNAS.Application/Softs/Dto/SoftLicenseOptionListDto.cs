using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftLicenseOptionListDto
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

    }
}
