using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Dictionarys.Dto
{
    public class DictionaryListDto
    {
        public virtual int Id { get; set; }

        public virtual string Type { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// 字典描述
        /// </summary>
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

        public DateTime CreationTime { get; set; }
    }
}
