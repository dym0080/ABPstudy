using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserUseLicenseInput : IInputDto
    {
        public long SoftUserId { get; set; }
        public string LicenseNo { get; set; }
    }
}