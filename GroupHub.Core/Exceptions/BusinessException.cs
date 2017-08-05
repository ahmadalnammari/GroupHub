using System;
using System.Collections.Generic;
using System.Configuration;

namespace GroupHub.Core
{
    [Serializable]
    public class BussinessException : Exception
    {
        public string MessageCode { get; set; }
        public virtual List<ErrorData> Errors { get; set; }
        public BussinessException(string meesageCode, string exception = null, object data = null)
        {
            if (Errors == null)
            {
                Errors = new List<ErrorData>();
            }

            Errors.Add(new ErrorData(meesageCode, this, data));
            MessageCode = meesageCode;
        }

        public BussinessException(List<ErrorData> _bussinessExceptions)
        {
            Errors = _bussinessExceptions;

        }
    }
    public class ErrorData
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
        public string MessageCode { get; set; }
        public object StackTrace { get; set; }

        public ErrorData(string messageCode)
        {
            //Message = ResourceManager.Instance().GetResourceValueByResourceType(messageCode, ResourceType.SystemMessage);
        }
        public ErrorData(string messageCode, Exception ex)
        {
            //Message = ResourceManager.Instance().GetResourceValueByResourceType(messageCode, ResourceType.SystemMessage);

            bool enableStackTrace = false;
            if (ConfigurationManager.AppSettings["EnableStackTrace"] != null)
            {
                 enableStackTrace = bool.Parse(ConfigurationManager.AppSettings["EnableStackTrace"]);
            }

            if (enableStackTrace)
            {
                Error = ex.Message;
                StackTrace = ex.StackTrace;
            }

            MessageCode = messageCode;


        }
        public ErrorData(string messageCode, Exception ex, object data)
        {
            //Message = ResourceManager.Instance().GetResourceValueByResourceType(messageCode, ResourceType.SystemMessage);

            bool enableStackTrace = false;
            if (ConfigurationManager.AppSettings["EnableStackTrace"] != null)
            {
                enableStackTrace = bool.Parse(ConfigurationManager.AppSettings["EnableStackTrace"]);
            }

            if (enableStackTrace)
            {
                StackTrace = ex.StackTrace;
                Error = ex.Message;
            }

            Data = data;
            MessageCode = messageCode;

        }

        public ErrorData(Exception ex)
        {

            bool enableStackTrace = false;
            if (ConfigurationManager.AppSettings["EnableStackTrace"] != null)
            {
                enableStackTrace = bool.Parse(ConfigurationManager.AppSettings["EnableStackTrace"]);
            }

            if (enableStackTrace)
            {
                StackTrace = ex.StackTrace;
                Error = ex.Message;
            }
        }

    }
}
