using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserLicenseMcodeListDto
    {
        public virtual long Id { get; set; }

        public virtual string Mcode { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }
        
    }
}
