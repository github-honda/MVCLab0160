using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.Web.Mvc;
using System.Text;
using MVCBase.Models;
using MVCBase.ViewModels;

namespace MVCBase.Controllers
{
    public class P0020Controller : Controller
    {
        public ActionResult Index()
        {
            // 多筆清單顯示. Controller預設action為Index, 詳於~/App_Start/RouteConfig.cs.
            return View(new P0020().Index());
        }
        public ActionResult Read(string id)
        {
            // 單筆顯示
            return View(new P0020().Read(id));
        }
        public ActionResult Create()
        {
            // 顯示新增網頁
            return View(new P0020().ConvertModelToViewModel(new T0010()));
        }
        public ActionResult Update(string id)
        {
            // 顯示修改畫面
            return View(new P0020().Read(id));
        }
        public ActionResult Delete(string id)
        {
            // 顯示刪除畫面
            return View(new P0020().Read(id));
        }
        public ActionResult TestModelBinder()
        {
            // 測試Model Binder
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitCreate(T0010 t1, string btnSubmit)
        {
            // 執行新增. 示範由ModelBinder取得輸入的欄位, 並將處理結果保留在ModelState中.
            int mi1, mi2;
            switch (btnSubmit)
            {
                case "新增":
                    // 無論是MoldelBinder或是商業邏輯的錯誤, 都收集到ModelState中, 再交由VIEW顯示.
                    if (ModelState.IsValid)
                    {
                        P0020 oP0020 = new P0020();
                        if (!oP0020.Create(t1)) // 商業邏輯
                            if (string.IsNullOrEmpty(oP0020.msError))
                                ModelState.AddModelError("P0020", "新增失敗, 請聯絡系統管理人員取得失敗原因.");
                            else
                                ModelState.AddModelError("P0020", oP0020.msError);
                    }
                    // 若有錯誤時, 則留在原畫面並顯示錯誤資訊.
                    if (!ModelState.IsValid) 
                    {
                        // 保留輸入資料=(將已接收的欄位資料, 轉到目前的ViewModel欄位), 再交由View顯示.
                        // 未通過ModelBinder檢核的欄位值, 可由ModelState["name"].Value.AttemptedValue取得.
                        // 若無法存回ViewModel欄位時, 例如: 若輸入abc字串, 卻要存到int欄位時, 只能以0存回, 不然就是要變更ViewModel欄位為string才能保留.
                        t1.ms1 = ModelState["ms1"].Value.AttemptedValue;
                        t1.ms2 = ModelState["ms2"].Value.AttemptedValue;
                        if (!int.TryParse(ModelState["mi1"].Value.AttemptedValue, out mi1))
                            mi1 = 0;
                        if (!int.TryParse(ModelState["mi2"].Value.AttemptedValue, out mi2))
                            mi2 = 0;
                        t1.mi1 = mi1;
                        t1.mi2 = mi2;
                        P0010ViewModel vm1 = new P0020().ConvertModelToViewModel(t1);

                        ViewBag.ErrorList = new CMN0010().GetModelStateErrorList(ModelState); // 示範如何顯示所有的錯誤.
                        return View("Create", vm1);
                    }
                    break;
            }
            return RedirectToAction("Index"); // 新增成功後或其他情況, 則轉到清單畫面.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitUpdate(FormCollection form1)  // 參數中不放任何model或變數, 就可以解除ModelBinder自動處理.
        {
            // 執行修改或刪除. 示範部份ModelBinder取得資料, 部份自行取得輸入欄位內容.
            // 1. Action函數中, 移除所有的(Model或變數)參數, 就可以解除ModelBinder自動接收欄位資料.
            // 2. TryValidateModel()或TryUpdateModel()也可指定要由ModelBinder處理接收資料的欄位. 
            // 3. ModelBinder沒有處理的欄位, 可從Request.Form或FormCollection取值. 建議使用FormCollection取值.
            T0010 t1 = new T0010();
            string s1, s2;
            int mi1, mi2;
            mi1 = 0;
            mi2 = 0;

            string btnSubmit = Request.Form["btnSubmit"];
            btnSubmit = form1["btnSubmit"]; // 結果同上. 建議使用FormCollection取值.
            switch (btnSubmit)
            {
                case "修改":
                    TryUpdateModel(t1, new string[] { "ms1", "ms2" }); // 指定要由ModelBinder處理的欄位.

                    // 本案例因商業邏輯只能接收int type的欄位值, 因此必須在傳給商業邏輯前, 處理Data Type的轉換問題.
                    s1 = form1["mi1"];
                    if (!int.TryParse(s1, out mi1))
                        ModelState.AddModelError("mi1", string.Format("欄位國文分數應該輸入數字, 不應該輸入{0}.", s1));
                    t1.mi1 = mi1;

                    s2 = form1["mi2"];
                    if (!int.TryParse(s2, out mi2))
                        ModelState.AddModelError("mi2", string.Format("欄位英文分數應該輸入數字, 不應該輸入{0}.", s2));
                    t1.mi2 = mi2;

                    // 無論是MoldelBinder或是商業邏輯的錯誤, 都收集到ModelState中, 再交由VIEW顯示.
                    if (ModelState.IsValid)
                    {
                        P0020 oP0020 = new P0020();
                        if (!oP0020.Update(t1)) // 商業邏輯
                            if (string.IsNullOrEmpty(oP0020.msError))
                                ModelState.AddModelError("P0020", "修改失敗, 請聯絡系統管理人員取得失敗原因.");
                            else
                                ModelState.AddModelError("P0020", oP0020.msError);
                    }
                    // 若有錯誤時, 則留在原畫面並顯示錯誤資訊.
                    if (!ModelState.IsValid)
                    {
                        // 保留輸入資料=(將已接收的欄位資料, 轉到目前的ViewModel欄位), 再交由View顯示.
                        // 未通過ModelBinder檢核的欄位值, 可由ModelState["name"].Value.AttemptedValue取得.
                        // 若無法存回ViewModel欄位時, 例如: 若輸入abc字串, 卻要存到int欄位時, 只能以0存回, 不然就是要變更ViewModel欄位為string才能保留.
                        t1.ms1 = ModelState["ms1"].Value.AttemptedValue;
                        t1.ms2 = ModelState["ms2"].Value.AttemptedValue;
                        t1.mi1 = mi1;
                        t1.mi2 = mi2;
                        P0010ViewModel vm1 = new P0020().ConvertModelToViewModel(t1);

                        ViewBag.ErrorList = new CMN0010().GetModelStateErrorList(ModelState); // 示範如何顯示所有的錯誤.
                        return View("Update", vm1);
                    }
                    break;
            }
            return RedirectToAction("Index"); // 修改或刪除成功後或其他情況, 則轉到清單畫面.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitDelete(string id, string btnSubmit)  // 參數中不放任何model或變數, 就可以解除ModelBinder自動處理.
        {
            // 刪除. (這一版本將Delete View獨立出來, 不再跟update view 合併)
            //string btnSubmit = Request.Form["btnSubmit"];
            //btnSubmit = form1["btnSubmit"]; // 結果同上. 建議使用FormCollection取值.
            string sKey1 = id;
            switch (btnSubmit)
            {
                case "刪除":
                    // 執行刪除.
                    // 刪除只需要取得key欄位, 就可以交給商業邏輯處理.
                    //t1.ms1 = form1["ms1"]; // 因Delete View現在已經不跟update view 合併, 只剩下Key值, 因此可以交由ModelBinder處理
                    if (string.IsNullOrEmpty(sKey1))
                        ModelState.AddModelError("ms1", string.Format("Key值不可空白."));

                    // 無論是MoldelBinder或是商業邏輯的錯誤, 都收集到ModelState中, 再交由VIEW顯示.
                    if (ModelState.IsValid)
                    {
                        P0030 oP0030 = new P0030();
                        if (!oP0030.Delete(sKey1)) // 商業邏輯
                            if (string.IsNullOrEmpty(oP0030.msError))
                                ModelState.AddModelError(oP0030.ToString(), "刪除失敗, 請聯絡系統管理人員取得失敗原因.");
                            else
                                ModelState.AddModelError(oP0030.ToString(), oP0030.msError);
                    }
                    // 若有錯誤時, 則留在原畫面並顯示錯誤資訊.
                    if (!ModelState.IsValid)
                    {
                        // 保留輸入資料其實就是讓ViewModel取得已經輸入欄位資料.
                        // 由於我們是把刪除跟修改的畫面作在同一個畫面
                        // 因此在取得key值欄位以後, 重新讀取資料庫, 更新原有的畫面即可.
                        P0010ViewModel vm1 = new P0020().ConvertModelToViewModel(new T0010().Read1Record(sKey1));

                        ViewBag.ErrorList = new CMN0010().GetModelStateErrorList(ModelState); // 示範如何顯示所有的錯誤.
                        return View("Delete", vm1);
                    }
                    break;
            }
            return RedirectToAction("Index"); // 修改或刪除成功後或其他情況, 則轉到清單畫面.
        }
        public string TestSubmitModelBinder(TestModelBinder e, string ms5, string btnSubmit)
        {
            // ModelBinder會自動比對(URL接收欄位)與(action函數參數欄位), 相同名稱的欄位會自動填入接收的資料內容.
            // (action函數參數欄位)比對範圍為: (函數中的參數名稱)以及(class中的所有property欄位, 包括子class). 
            // 也可由Request.Form["欄位名稱"]方式取得接收資料.
            return "ms1=" + e.ms1 + // 比對property名稱符合
                "<br />, ms2=" + e.ms2 + // 比對property名稱符合
                "<br />, ms3=" + e.ms3 + // 比對property名稱符合
                "<br />, ms4=" + e.ms4 + // 比對property名稱符合
                "<br />, ms5=" + ms5 + // 比對函數參數名稱符合.
                "<br />, ms6=" + Request.Form["ms6"] + // 自行取得
                "<br />, mT0010.ms1=" + e.mT0010.ms1 + // 比對子class欄位.
                "<br />, mT0010.ms2=" + e.mT0010.ms2 + // 比對子class欄位.
                "<br />, mT0010.mi1=" + e.mT0010.mi1 + // 比對子class欄位.
                "<br />, mT0010.mi2=" + e.mT0010.mi2 + // 比對子class欄位.
                "<br />, btnSumit=" + btnSubmit // 比對函數參數名稱符合.
                + new CMN0010().GetModelStateErrorList(ModelState);
        }

    }
}



