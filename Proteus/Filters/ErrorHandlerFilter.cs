using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proteus.Filters
{
    public class ErrorHandlerFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                LogError(filterContext);

                base.OnException(filterContext);

                filterContext.ExceptionHandled = true;
            }
        }

        private void LogError(ExceptionContext filterContext)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/ErrorLogs")))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/ErrorLogs"));

            var exceptionMessage = filterContext.Exception.Message;
            var stackTrace = filterContext.Exception.StackTrace;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            string Message = "Date :" + DateTime.Now.ToString() + ", Controller: " + controllerName + ", Action: " + actionName + Environment.NewLine +
                 "Error Message : " + exceptionMessage + Environment.NewLine + "Stack Trace :" + Environment.NewLine
                 + stackTrace + Environment.NewLine + Environment.NewLine;

            File.AppendAllText(HttpContext.Current.Server.MapPath("~/ErrorLogs/ErrorLog.txt"), Message);
        }
    }
}