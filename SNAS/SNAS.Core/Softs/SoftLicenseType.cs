using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Core.Softs
{
    /// <summary>
    /// 软件卡密类型
    /// </summary>
    public enum SoftLicenseType
    {
        /// <summary>
        /// 月
        /// </summary>
        Month = 0,

        /// <summary>
        /// 周
        /// </summary>
        Week = 1,

        /// <summary>
        /// 天
        /// </summary>
        Day = 2,

        /// <summary>
        /// 年
        /// </summary>
        Year = 3,

        /// <summary>
        /// 小时
        /// </summary>
        Hour = 4,
        /// <summary>
        /// 永久
        /// </summary>
        Forever

    }
}
