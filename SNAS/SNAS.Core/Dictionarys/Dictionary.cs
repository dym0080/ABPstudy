using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Core.Dictionarys
{
    [Table("Dictionary")]
    public class Dictionary: AuditedEntity<long>, ISoftDelete
    {
        public const int MaxLenght50 = 50;
        public const int MaxLenght200 = 200;

        /// <summary>
        /// 字典Id
        /// </summary>
        //[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)] //主键 自增
        //public virtual int Id { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [StringLength(MaxLenght50)]
        public virtual string Type { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [Required, StringLength(MaxLenght200)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [StringLength(MaxLenght50)]
        public virtual string Value { get; set; }

        /// <summary>
        /// 字典描述
        /// </summary>
        [StringLength(MaxLenght200)]
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
