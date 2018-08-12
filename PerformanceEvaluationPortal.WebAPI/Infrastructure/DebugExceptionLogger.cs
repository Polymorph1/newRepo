using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace PerformanceEvaluationPortal.WebAPI.Infrastructure
{
    public class DebugExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            System.Diagnostics.Debug.WriteLine("Exception: " + context.ExceptionContext.Exception.Message);
        }
    }
}