using System.Web;
using System.Web.Mvc;
// add
using MVCBase.Filter; // for CMNExceptionFilter() 

namespace MVCBase
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // global filter for ExceptionFilter. Same as [HandlerError] in each Controller level or action.
            //filters.Add(new HandleErrorAttribute()); // 預設的exception處理程序
            filters.Add(new CMNExceptionFilter()); // CMNExceptionFilter繼承了HandleErrorAttribute, 並增加寫出Log處理.
            filters.Add(new CMNActionFilter());
        }
    }
}
