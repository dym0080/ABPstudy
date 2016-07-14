using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using SNAS.Core.SoftUsers;

namespace SNAS.Core.Finances
{

    /// <summary>
    /// 财务记录
    /// </summary>
    [Table("Finance")]
    public class Finance : AuditedEntity<long>
    {

        /// <summary>
        ///备注最大长度
        /// </summary>
        public const int MaxRemarkLength = 500;

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Money { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual FinanceType Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(MaxRemarkLength)]
        public virtual string Remark { get; set; }

    }
}
