using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserLicenseListDto
    {

        public virtual long Id { get; set; }

        public virtual string LoginName { get; set; }

        public virtual string SoftName { get; set; }

        public virtual string QQ { get; set; }

        public bool IsActive { get; set; }

        public DateTime AuthorizeTime { get; set; }

        public DateTime? ExpireTime { get; set; }

        public virtual SoftUserLicenseType Type { get; set; }


        public string TypeAlias
        {
            get
            {
                switch (Type)
                {
                    case SoftUserLicenseType.Free:
                        return "免费";
                    case SoftUserLicenseType.Fee:
                        return "收费";
                    case SoftUserLicenseType.Trial:
                        return "试用";
                    default:
                        return "";
                }
            }
        }
    }
}
