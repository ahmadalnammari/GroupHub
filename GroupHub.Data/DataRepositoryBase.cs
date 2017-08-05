

using GroupHub.Core;
using GroupHub.Data;

namespace GroupHub.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, GroupHubContext>
        where T : class, IIdentifiableEntity, new()
    {
    }
}
