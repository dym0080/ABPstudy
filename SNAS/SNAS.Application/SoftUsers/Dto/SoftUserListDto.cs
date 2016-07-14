using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserListDto
    {

        public virtual long Id { get; set; }

        public virtual string LoginName { get; set; }

        public virtual string Mobile { get; set; }

        public virtual string QQ { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual SoftUserSource Source { get; set; }


        public string SourceAlias
        {
            get
            {
                switch (Source)
                {
                    case SoftUserSource.Register:
                        return "用户注册";
                    case SoftUserSource.System:
                        return "后台添加";
                    default:
                        return "";
                }
            }
        }
    }
}
