using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBase.Models;
using MVCBase.ViewModels;

namespace MVCBase.Controllers
{
    public class P0010Controller : Controller
    {
        public ActionResult TestView2(string id)
        {
            // Controller負責(控制流程):
            // 商業邏輯P0010物件, 使用BusinessTestView2()方法, 產出P0010ViewModel物件到View.
            P0010 p1 = new P0010();
            P0010ViewModel vm1 = p1.BusinessTestView2(id);
            return View(vm1);
        }
        public ActionResult TestView()
        {
            // Controller負責(控制流程):
            // 商業邏輯P0010物件, 使用BusinessTestView()方法, 產出P0010ListViewModel物件到View.
            return View(new P0010().BusinessTestView());
        }

        public ActionResult GetView()
        {
            // 回傳(由~/Views/P0010/TestView.cshtml產生的HTML string).
            return View("TestView");
        }
        public ActionResult GetView2(string id)
        {
            // id是route"{controller}/{action}/{id}"預設的參數名稱, 可接收自URL.
            if (id == "2")
                return View("TestView2");
            else
                return View("TestView");
        }
        public ActionResult GetContent()
        {
            // 回傳Content爲string.
            return Content("P0010Controller.GetContent() is here.");
        }

        // GET: P0010
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public string GetString1()
        {
            return "Controller.ActionMethod=P0010.GetString1().";
        }
        public string GetString2()
        {
            return "Controller.ActionMethod=P0010.GetString2().";
        }
      
        public DateTime GetNow()
        {
            // 若非回傳string, 則會自動以ToString()回傳. 
            return DateTime.Now;
        }

        [NonAction]
        public string GetString3()
        {
            // 若設定屬性=[NonAction]的方法，則不會成爲Controller.Action.
            return "若設定屬性=[NonAction]的方法，則不會成爲Controller.Action.";
        }

        string GetString4()
        {
            return "若非public的方法, 就不是Controller.Action";
        }
    }
}