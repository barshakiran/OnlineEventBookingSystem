using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace OnlineEventBookingSystemAPI.CustomException
{
    public class CustomExceptionFilterApi:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string errMsg = string.Empty;
            var exceptionType = actionExecutedContext.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                errMsg = "Unauthorized access";
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                errMsg = "Data is not found";
                statusCode = HttpStatusCode.NotFound;
            }
            
            else
            {
                errMsg = "Contact to you server administrator";
                statusCode = HttpStatusCode.InternalServerError;
            }
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(errMsg),
                ReasonPhrase = "From Api exception filter"
            };
            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }
    }
}