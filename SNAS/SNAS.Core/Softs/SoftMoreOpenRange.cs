using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Core.Softs
{
    /// <summary>
    /// 软件多开范围
    /// </summary>
    public enum SoftMoreOpenRange
    {
        /// <summary>
        /// 同一机器
        /// </summary>
        Machine = 0,

        /// <summary>
        /// 同ip
        /// </summary>
        Ip = 1,

        /// <summary>
        /// 所有电脑
        /// </summary>
        All=2
    }
}
