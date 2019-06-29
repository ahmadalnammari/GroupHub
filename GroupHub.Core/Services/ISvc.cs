using GroupHub.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Core.Services
{
    public interface ISvc
    {

    }

    public interface ISvc<T> : ISvc where T : class, IDomain
    {

    }
}
