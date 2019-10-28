using GroupHub.Core.Domain;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Infra.Services
{
	/// <typeparam name="T"></typeparam>
	public abstract class SvcBase<T> : Svc where T : class, IDomain
    {
        public SvcBase(GroupHubContext groupHubContext) :base(groupHubContext) { }
    }

}
