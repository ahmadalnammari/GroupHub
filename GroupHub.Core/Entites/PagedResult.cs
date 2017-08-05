
using System.Collections.Generic;

namespace GroupHub.Core
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int CurrentPage { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPagesCount { get; set; }
    }
}
