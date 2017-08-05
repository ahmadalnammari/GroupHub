using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using GroupHub.Core;

namespace GroupHub.Apis.Core
{
    public class ApiControllerBase : ApiController, IServiceAwareController
    {
        List<IServiceContract> _DisposableServices;

        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {
        }

        void IServiceAwareController.RegisterDisposableServices(List<IServiceContract> disposableServices)
        {
            RegisterServices(disposableServices);
        }

        List<IServiceContract> IServiceAwareController.DisposableServices
        {
            get
            {
                if (_DisposableServices == null)
                    _DisposableServices = new List<IServiceContract>();

                return _DisposableServices;
            }
        }
        protected override ExceptionResult InternalServerError(Exception exception)
        {
            return base.InternalServerError(exception);
        }


        protected override InvalidModelStateResult BadRequest(ModelStateDictionary modelState)
        {
            return base.BadRequest(modelState);
        }

        protected void ValidateAuthorizedUser(string userRequested)
        {
            string userLoggedIn = User.Identity.Name;
            if (userLoggedIn != userRequested)
                throw new SecurityException("Attempting to access data for another user.");
        }


        protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {
         
                HttpResponseMessage response = null;
                response = new HttpResponseMessage(HttpStatusCode.OK);
                return codeToExecute.Invoke();
            
          
           

        }

      
    }
 
}