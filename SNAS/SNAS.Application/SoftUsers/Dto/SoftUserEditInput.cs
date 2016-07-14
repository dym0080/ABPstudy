using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserEditInput : IInputDto
    {
        public long Id { get; set; }

        [Required]
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxLoginNameLength)]
        public virtual string LoginName { get; set; }
        
        /// <summary>
        /// �ֻ�����
        /// </summary>
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxMobileLength)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// QQ����
        /// </summary>
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxQQLength)]
        public virtual string QQ { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [StringLength(SNAS.Core.SoftUsers.SoftUser.MaxRemarkLength)]
        public virtual string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}