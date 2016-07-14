using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using SNAS.Core.Softs;

namespace SNAS.Core.SoftUsers
{
    /// <summary>
    /// 用户登录记录
    /// </summary>
    [Table("SoftUserLogin")]
    public class SoftUserLogin : CreationAuditedEntity<long>
    {
        /// <summary>
        /// 用户名称最大长度
        /// </summary>
        public const int MaxLoginNameLength = 50;

        /// <summary>
        /// Ip最大长度
        /// </summary>
        public const int MaxIpLength = 15;

        [Column(Order = 0)]
        [ForeignKey("SoftUser")]
        [Required]
        public long SoftUserId { get; set; }

        /// <summary>
        /// 登录用户
        /// </summary>
        [Required]
        public virtual SoftUser SoftUser { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }

        /// <summary>
        /// 登录软件
        /// </summary>
        [Required]
        public virtual Soft Soft { get; set; }


        /// <summary>
        /// Ip
        /// </summary>
        [Required]
        [StringLength(MaxIpLength)]
        public virtual string Ip { get; set; }

        /// <summary>
        /// 机器码
        /// </summary>
        [StringLength(32)]
        public virtual string Mcode { get; set; }


    }
}
