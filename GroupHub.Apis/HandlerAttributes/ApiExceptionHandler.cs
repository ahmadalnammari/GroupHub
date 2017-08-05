using System;
using System.Net;
using System.Security;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using System.Net.Http;
using System.ServiceModel;
using GroupHub.Core;
using GroupHub.Common;

namespace Tahaluf.eChannels.Apis
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            try
            {
                if (context.Exception is SecurityException)
                {
                    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.Unauthorized, new ErrorData(SystemErrors.UnhandledException, context.Exception)));
                    return;

                }
                if (context.Exception is FaultException)
                {
                    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorData(SystemErrors.UnhandledException, context.Exception)));
                    return;
                }
                if (context.Exception is BussinessException)
                {
                    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, ((BussinessException)context.Exception).Errors));
                    return;
                }
                if (context.Exception is UnauthorizedAccessException)
                {
                    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.Forbidden));
                    return;
                }
                if (context.Exception is Exception)
                {
                    context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorData(SystemErrors.UnhandledException, context.Exception)));
                    return;
                }
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorData(SystemErrors.UnhandledException, context.Exception)));

            }
            catch (Exception ex)
            {
                //ex.Log();
                context.Result = new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorData(context.Exception)));
            }
        }


        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

    }
}