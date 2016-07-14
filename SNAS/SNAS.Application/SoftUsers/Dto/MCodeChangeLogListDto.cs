using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class MCodeChangeLogListDto
    {

        public virtual long Id { get; set; }

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

        public DateTime CreationTime { get; set; }


        public string SourceAlias
        {
            get
            {
                switch (Source)
                {
                    case MCodeChangeSource.System:
                        return "后台更换";
                    case MCodeChangeSource.User:
                        return "用户自助";
                    default:
                        return "";
                }
            }
        }



    }
}
