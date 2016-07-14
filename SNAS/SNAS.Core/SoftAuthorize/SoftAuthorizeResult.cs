using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Core.SoftAuthorize
{
    /// <summary>
    /// 软件授权结果
    /// </summary>
    public class SoftAuthorizeResult
    {
        /// <summary>
        /// 结果代号
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 失败时说明
        /// </summary>
        public string errormsg { get; set; }

        /// <summary>
        /// 成功时说明(可空)
        /// </summary>
        public string msg { get; set; }
    }
}
