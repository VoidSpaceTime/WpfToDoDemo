﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared
{
    public interface IPagedList<T>
    {
        /// <summary>
        /// Gets the index start value.
        /// 获取索引起始值
        /// </summary>
        /// <value>The index start value.</value>
        int IndexFrom { get; }
        /// <summary>
        /// Gets the page index (current).
        /// 获取页索引(当前)
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// Gets the page size.
        /// 获取页面大小
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// Gets the total count of the list of type <typeparamref name="T"/>
        /// 获取类型列表的总计数
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// Gets the total pages.
        /// 获取页面总数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// Gets the current page items.
        /// 获取当前页项
        /// </summary>
        IList<T> Items { get; }
        /// <summary>
        /// Gets the has previous page.
        /// 获取前一页
        /// </summary>
        /// <value>The has previous page.</value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets the has next page.
        /// 获取下一页
        /// </summary>
        /// <value>The has next page.</value>
        bool HasNextPage { get; }
    }
}
