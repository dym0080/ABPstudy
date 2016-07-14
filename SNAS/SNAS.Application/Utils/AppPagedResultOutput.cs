using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SNAS.Application.Softs.Dto;

namespace SNAS.Application.Utils
{
    public class AppPagedResultOutput<T> : PagedResultOutput<T>
    {
        private int pageindex = 1;
        private int pagesize = 10;

        public AppPagedResultOutput(int totalCount, IReadOnlyList<T> items,int pageIndex,int pageSize)
            : base(totalCount, items)
        {
            this.pageindex = pageIndex;
            this.pagesize = pageSize;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get { return pageindex; }
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize => pagesize;

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(((double)TotalCount / PageSize));
            }
        }

        /// <summary>
        /// 开始序号
        /// </summary>
        public int Begin
        {
            get { return PageSize * (PageIndex - 1) + 1; }
        }

        /// <summary>
        /// 结束序号
        /// </summary>
        public int End
        {
            get
            {
                return Math.Min(TotalCount, PageSize * PageIndex);
            }
        }
    }
}
