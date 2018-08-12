using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace PerformanceEvaluationPortal.WebAPI.Infrastructure
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public async override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage();

            if (context.Exception is ArgumentException)
            {
                response = context.Request.CreateResponse(HttpStatusCode.Forbidden,
                new
                {
                    Message = context.Exception.Message
                });
                response.Headers.Add("X-Error", "Invalid input");
            }

            if (context.Exception is ApplicationException)
            {
                response = context.Request.CreateResponse(HttpStatusCode.NotFound,
                new
                {
                    Message = context.Exception.Message
                });
                response.Headers.Add("X-Error", "Invalid input");
            }

            if (context.Exception is ArgumentOutOfRangeException)
            {
                response = context.Request.CreateResponse(HttpStatusCode.NotFound,
                new
                {
                    Message = context.Exception.Message
                });
                response.Headers.Add("X-Error", "Invalid input");
            }

            if (context.Exception is InvalidOperationException)
            {
                response = context.Request.CreateResponse(HttpStatusCode.Forbidden,
                new
                {
                    Message = context.Exception.Message
                });
                response.Headers.Add("X-Error", "Invalid input");
            }

            if (context.Exception is UnauthorizedAccessException)
            {
                response = context.Request.CreateResponse(HttpStatusCode.Unauthorized,
                new
                {
                    Message = context.Exception.Message
                });
                response.Headers.Add("X-Error", "Invalid input");
            }

            else
            {
                const string genericErrorMessage = "An unexpected error occured.";
                response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                new
                {
                    Message = genericErrorMessage
                });
                
                response.Headers.Add("X-Error", genericErrorMessage);
            }

            context.Result = new ResponseMessageResult(response);
        }
    }
}