using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftListDto
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        /// <summary>
        /// appId
        /// </summary>
        public virtual string AppId { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public virtual string AppSecret { get; set; }

        /// <summary>
        /// 软件绑定模式
        /// </summary>
        public virtual SoftBindMode BindMode { get; set; }

        /// <summary>
        /// 软件收费模式
        /// </summary>

        public virtual SoftRunMode RunMode { get; set; }


        /// <summary>
        /// 是否有效
        /// </summary>

        public bool IsActive { get; set; }


        public DateTime CreationTime { get; set; }


        public string BindModeAlias
        {
            get
            {
                switch (BindMode)
                {
                    case SoftBindMode.BindMachine:
                        return "绑定机器";
                    case SoftBindMode.UnBindMachine:
                        return "不绑定机器";
                    default:
                        return "";
                }
            }
        }

        public string RunModeAlias
        {
            get
            {
                switch (RunMode)
                {
                    case SoftRunMode.Free:
                        return "免费";
                    case SoftRunMode.Fee:
                        return "收费";
                    default:
                        return "";
                }
            }
        }
    }
}
