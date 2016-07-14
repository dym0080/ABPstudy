using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Utils
{
    public class FilterCondition
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 逻辑操作符
        /// </summary>
        public string logic { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public string @operator { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 筛选值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 筛选最小值
        /// </summary>
        public string minValue { get; set; }

        /// <summary>
        /// 筛选最大值
        /// </summary>
        public string maxValue { get; set; }

        /// <summary>
        /// 不参与自动拼接
        /// </summary>
        public bool ignore { get; set; }
    }
}
