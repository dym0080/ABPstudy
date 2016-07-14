using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftItemDto
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

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

        public string Remark { get; set; }

    }
}
