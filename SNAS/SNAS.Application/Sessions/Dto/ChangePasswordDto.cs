using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SNAS.Application.Users;
using SNAS.Core.Users;

namespace SNAS.Application.Sessions.Dto
{
    
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPassword2 { get; set; }
    }
}
