using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Dictionarys.Dto
{
    public class DictionaryEditInput:IInputDto
    {
        public virtual int Id { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [StringLength(50)]
        public virtual string Type { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [Required, StringLength(200)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [StringLength(50)]
        public virtual string Value { get; set; }

        /// <summary>
        /// 字典描述
        /// </summary>
        [StringLength(200)]
        public virtual string Description { get; set; }

        /// <summary>
        /// 字典排序
        /// </summary>
        public virtual int? OrderId { get; set; }

        /// <summary>
        /// 字典父类Id
        /// </summary>
        public virtual int ParentId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { set; get; }
    }
}
