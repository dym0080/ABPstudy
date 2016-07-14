using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Softs.Dto
{
    /// <summary>
    /// 卡密生成
    /// </summary>
    public class SoftLicenseGenerateItemInput
    {
        /// <summary>
        /// 卡密类型id
        /// </summary>
        public long SoftLicenseOptionId { get; set; }

        /// <summary>
        /// 生成卡密数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 软件id
        /// </summary>
        public long SoftId { get; set; }
    }
}
