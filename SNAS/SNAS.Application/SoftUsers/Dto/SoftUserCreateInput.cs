using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserCreateInput : IInputDto
    {
        [Required]
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxLoginNameLength)]
        public virtual string LoginName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required]
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxPasswordLength)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxMobileLength)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxQQLength)]
        public virtual string QQ { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxRemarkLength)]
        public virtual string Remark { get; set; }
    }
}