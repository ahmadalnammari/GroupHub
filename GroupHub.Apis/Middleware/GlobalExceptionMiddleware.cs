using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Security;
using System.ServiceModel;
using System.Threading.Tasks;
using GroupHub.Common;
using GroupHub.Core;

namespace GroupHub.Apis
{
    public class GlobalExceptionMiddleware : OwinMiddleware
    {
        public GlobalExceptionMiddleware(OwinMiddleware next) : base(next)
        { }

        public override async Task Invoke(IOwinContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch (Exception exception)
            {

                if (exception is SecurityException)
                {
                    GenerateErrorResult(context, HttpStatusCode.Unauthorized, new FaultException<ErrorData>(new ErrorData(SystemErrors.UnhandledException, exception)));
                    return;
                }
                
                if (exception is FaultException<ErrorData>)
                {
                    GenerateErrorResult(context, HttpStatusCode.InternalServerError, ((FaultException<ErrorData>)exception));
                    return;
                }
                if (exception is FaultException)
                {
                    GenerateErrorResult(context, HttpStatusCode.InternalServerError, new FaultException<ErrorData>(new ErrorData(SystemErrors.UnhandledException, exception)));
                    return;
                }
                
                if (exception is UnauthorizedAccessException)
                {
                    GenerateErrorResult(context, HttpStatusCode.Forbidden, new FaultException<ErrorData>(new ErrorData(SystemErrors.UnhandledException, exception)));
                    return;
                }

                if (exception is UnauthorizedAccessException)
                {
                    GenerateErrorResult(context, HttpStatusCode.Forbidden, new FaultException<ErrorData>(new ErrorData(SystemErrors.UnhandledException, exception)));
                    return;
                }

                GenerateErrorResult(context, HttpStatusCode.InternalServerError, new FaultException<ErrorData>(new ErrorData(SystemErrors.UnhandledException, exception)));
            }
            
        }

        private void GenerateErrorResult(IOwinContext context, HttpStatusCode code, Object obj)
        {
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
    }
}