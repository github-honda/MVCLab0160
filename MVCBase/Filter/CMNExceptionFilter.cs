using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.Web.Mvc;
using MVCBase.Models;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace MVCBase.Filter
{
    public class CMNExceptionFilter: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string sTime = DateTime.Now.ToString("yyyyMMdd-HHmmss-fff");
            string sDate = sTime.Substring(0, 8);
            string sMsg = string.Format("OnException(): Controller.Action={0}.{1}, Message={2}",
                filterContext.RouteData.Values["controller"],
                filterContext.RouteData.Values["action"],
                filterContext.Exception.Message);
            Debug.WriteLine(sMsg);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--------------------------------");
            sb.AppendLine(sTime);
            sb.AppendLine(sMsg);
            sb.AppendLine(filterContext.Exception.StackTrace);
            sb.AppendLine();
            string sFile = filterContext.HttpContext.Server.MapPath(string.Format(@"~/App_Data/Exception-{0}.txt", sDate));
            File.AppendAllText(sFile, sb.ToString());

            base.OnException(filterContext);
        }
    }
}


