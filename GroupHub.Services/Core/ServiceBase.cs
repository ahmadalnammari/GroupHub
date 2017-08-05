
using GroupHub.Core;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceModel;

namespace Tahaluf.ElasticSearch.Services.Core
{
    public class ServiceBase : IDisposable, IServiceContract
    {

        public ServiceBase()
        {
            OperationContext context = OperationContext.Current;

            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute, bool isCachable = false, string key = "")
        {
            try
            {
                //if (isCachable)
                //{
                //    StackTrace stackTrace = new StackTrace();
                //    MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                //    string cacheKey = CacheKeyManager.GenerateCacheKey(methodBase, key, IdentityManager.GetCurrentLanguage());
                //    return ResourceManager.Instance().GetResource(codetoExecute, cacheKey, IdentityManager.GetCurrentLanguage());
                //}
                //else
                    return codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (BussinessException ex)
            {
                throw ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw new FaultException("Inner exception: " + ex.InnerException.Message + "\nMessage:" + ex.Message);
                }
                throw ex;
            }
        }

        protected void ExecuteFaultHandledOperation(System.Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (AuthorizationValidationException ex)
            {
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (BussinessException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ServiceBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
