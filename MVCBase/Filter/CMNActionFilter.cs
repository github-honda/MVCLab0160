using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Routing;

namespace MVCBase.Filter
{
    public class CMNActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            DebugActionFilter("OnActionExecuted()", filterContext.RouteData);
            base.OnActionExecuted(filterContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DebugActionFilter("OnActionExecuting()", filterContext.RouteData);
            base.OnActionExecuting(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            DebugActionFilter("OnResultExecuted()", filterContext.RouteData);
            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            DebugActionFilter("OnResultExecuting()", filterContext.RouteData);
            base.OnResultExecuting(filterContext);
        }
        private void DebugActionFilter(string sFunction, RouteData rdata1)
        {
            Debug.WriteLine(string.Format("{0}, controller.action={1}.{2}.",
                sFunction,
                rdata1.Values["controller"],
                rdata1.Values["action"]));
        }
    }
}

