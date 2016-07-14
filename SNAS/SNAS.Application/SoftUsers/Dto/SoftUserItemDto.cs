using System.ComponentModel.DataAnnotations;
using SNAS.Core.Softs;

namespace SNAS.Application.SoftUsers.Dto
{
    public class SoftUserItemDto
    {

        public virtual long Id { get; set; }

        public virtual string LoginName { get; set; }
        
        public virtual string Mobile { get; set; }
        
        public virtual string QQ { get; set; }
        
        public virtual string Remark { get; set; }

        public bool IsActive { get; set; }
    }
}
