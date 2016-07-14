using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Reports.Dto
{
    /// <summary>
    /// 日数据统计项目
    /// </summary>
   public class DateCountInfo
    {
        public string Date { get; set; }

        public decimal Count { get; set; }
    }
}
