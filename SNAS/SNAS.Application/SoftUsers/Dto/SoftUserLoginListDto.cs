using System;
using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;
using SNAS.Core.SoftUsers;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserLoginListDto
    {

        public virtual long Id { get; set; }

        public virtual string LoginName { get; set; }

        public virtual string SoftName { get; set; }

        public virtual string Ip { get; set; }

        public virtual string Mcode { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
