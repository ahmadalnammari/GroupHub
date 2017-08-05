using System.Collections.Generic;

namespace GroupHub.Core
{
    public interface IPagedResult<T>
    {
        IEnumerable<T> Results { get; set; }
        int CurrentPage { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        int TotalPagesCount { get; set; }
    }
}
