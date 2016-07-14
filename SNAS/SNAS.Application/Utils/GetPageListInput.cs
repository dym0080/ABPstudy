using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Utils
{
    /// <summary>
    /// 获取分页数据的条件
    /// </summary>
    public class GetPageListInput : GetListInput, IPagedResultRequest
    {
        public GetPageListInput() {

            PageSize = 10;
            CurrentPage = 1;
            
        }

        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int PageSize { get; set; }


        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPage { get; set; }


        public int SkipCount
        {
            get
            {
                return (CurrentPage - 1) * PageSize;
            }

            set
            {
                
            }
        }

        public int MaxResultCount
        {
            get
            {
                return PageSize;
            }

            set
            {
                PageSize = value;
            }
        }

    }
}
