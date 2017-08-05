using System.Collections.Generic;
using GroupHub.Core;

namespace GroupHub.Apis.Core
{
    public interface IServiceAwareController
    {
        void RegisterDisposableServices(List<IServiceContract> disposableServices);

        List<IServiceContract> DisposableServices { get; }
    }
}
