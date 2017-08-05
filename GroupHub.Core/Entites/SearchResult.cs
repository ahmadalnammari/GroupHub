using System.Collections.Generic;

namespace GroupHub.Core
{
    public  class PagedSearchResult<T> where T : class
    {
        public int NumberOfRecords { get; set; }

        public List<T> Collection { get; set; }
    }
}
