using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBase.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            Exception e1 = new Exception("網址錯誤. 找不到指定的網址.");
            HandleErrorInfo model1 = new HandleErrorInfo(e1, "Unknown", "Unknown");
            return View("Error", model1);
        }
    }
}

