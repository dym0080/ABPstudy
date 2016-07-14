using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using SNAS.Core.Softs;

namespace SNAS.Core.SoftUsers
{
    /// <summary>
    /// 软件在线记录
    /// </summary>
    [Table("SoftOnlineLog")]
    public class SoftOnlineLog : CreationAuditedEntity<long>
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
        public virtual SoftUser SoftUser { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Soft")]
        [Required]
        public long SoftId { get; set; }

        /// <summary>
        /// 登录软件
        /// </summary>
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

        /// <summary>
        /// 进程代号
        /// </summary>
        [StringLength(32)]
        public virtual string ProcessNo { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public virtual bool IsOnline { get; set; }
        
        /// <summary>
        /// 在线时长
        /// </summary>
        public virtual int OnlineTime { get; set; }

        /// <summary>
        /// 上次校验时间
        /// </summary>
        public virtual DateTime? LastCheckTime { get; set; }

        /// <summary>
        /// 上次下线时间
        /// </summary>
        public virtual DateTime? OfflineTime { get; set; }

        /// <summary>
        /// 下线原因
        /// </summary>
        public virtual SoftOfflineReason OfflineReason { get; set; }
    }
}
