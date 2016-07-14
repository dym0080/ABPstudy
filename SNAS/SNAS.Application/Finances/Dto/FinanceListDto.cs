using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAS.Core.Finances;

namespace SNAS.Application.Finances.Dto
{
    public class FinanceListDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Money { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual FinanceType Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }


        public DateTime CreationTime { get; set; }


        public string TypeAlias
        {
            get
            {
                switch (Type)
                {
                    case FinanceType.InCome:
                        return "收入";
                    case FinanceType.Expenses:
                        return "支出";
                    default:
                        return "";
                }
            }
        }
    }
}
