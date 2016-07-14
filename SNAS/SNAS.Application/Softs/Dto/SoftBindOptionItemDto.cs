using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.Softs.Dto
{
    public class SoftBindOptionItemDto
    {
        public long SoftId { get; set; }

        /// <summary>
        /// 允许绑定数量
        /// </summary>
        public virtual int AllowBindCount { get; set; }

        /// <summary>
        /// 允许换绑次数
        /// </summary>
        public virtual int AllowChangeBindCount { get; set; }

    }
}
