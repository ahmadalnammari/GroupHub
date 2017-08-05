using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupHub.Core
{
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IDataRepository;
    }
}
