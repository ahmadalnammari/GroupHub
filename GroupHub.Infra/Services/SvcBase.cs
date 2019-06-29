using GroupHub.Core.Domain;
using System.Collections.Generic;
using System.Text;

namespace GroupHub.Infra.Services
{
	/// <summary>
	/// TODO: Looks like its not of much use now, reconsider removing it
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SvcBase<T> : Svc where T : class, IDomain
    {
        public SvcBase():base() { }
    }

}
