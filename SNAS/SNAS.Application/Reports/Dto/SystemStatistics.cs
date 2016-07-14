using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Reports.Dto
{
    /// <summary>
    /// 系统统计信息
    /// </summary>
    public class SystemStatistics
    {
        /// <summary>
        /// 软件总数
        /// </summary>
        public int Soft { get; set; }

        /// <summary>
        /// 用户总数
        /// </summary>
        public int SoftUser { get; set; }

        /// <summary>
        /// 总收入
        /// </summary>
        public decimal TotalInCome { get; set; }

        /// <summary>
        /// 今日收入
        /// </summary>
        public decimal TodayInCome { get; set; }
    }
}
