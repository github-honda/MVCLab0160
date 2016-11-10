using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
// add
using System.Diagnostics;
using System.IO;
namespace MVCBase
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 以程式方式指定Trace.Listeners檔案, 先清除原設定再新增!
            while (Trace.Listeners.Count > 0)
                Trace.Listeners.RemoveAt(0);
            string sFile = Context.Server.MapPath(string.Format(@"~/App_Data/TraceLog-{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
            Trace.Listeners.Add(new TextWriterTraceListener(sFile));
            Trace.AutoFlush = true;

            Debug.WriteLine("Debug Begin." + DateTime.Now.ToString("yyyyMMdd.HHmmss.fff"));
            Trace.WriteLine("Trace Begin." + DateTime.Now.ToString("yyyyMMdd.HHmmss.fff"));

            // 預設程式碼如下不變
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
