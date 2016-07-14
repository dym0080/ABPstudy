using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Core.Softs
{
    /// <summary>
    /// 软件注册限制周期
    /// </summary>
    public enum SoftRegisterLimitCycle
    {
        /// <summary>
        /// 每天
        /// </summary>
        Day=0,

        /// <summary>
        /// 永久
        /// </summary>
        Forever = 1,
    }
}
