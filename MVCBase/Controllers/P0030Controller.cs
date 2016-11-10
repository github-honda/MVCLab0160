using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// add
using MVCBase.ViewModels;
using MVCBase.Models;


namespace MVCBase.Controllers
{
    public class P0030Controller : Controller
    {
        // GET: P0030
        public ActionResult Index()
        {
            // 多筆清單顯示. Controller預設action為Index, 詳於~/App_Start/RouteConfig.cs.
            return View(new P0030().Index());
        }
        public ActionResult Read(string id)
        {
            // 單筆顯示
            return View(new P0030().Read(id));
        }
        public ActionResult Create()
        {
            // 顯示新增網頁
            return View(new P0030().ConvertModelToViewModel(new T0010()));
        }
        public ActionResult Update(string id)
        {
            // 顯示修改畫面
            return View(new P0030().Read(id));
        }
        public ActionResult Delete(string id)
        {
            // 顯示刪除畫面
            return View(new P0030().Read(id));
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
                        P0030 oP0030 = new P0030();
                        if (!oP0030.Create(t1)) // 商業邏輯
                            if (string.IsNullOrEmpty(oP0030.msError))
                                ModelState.AddModelError("P0030", "新增失敗, 請聯絡系統管理人員取得失敗原因.");
                            else
                                ModelState.AddModelError("P0030", oP0030.msError);
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
                        P0030ViewModel vm1 = new P0030().ConvertModelToViewModel(t1);

                        return View("Create", vm1);
                    }
                    break;
            }
            return RedirectToAction("Index"); // 新增成功後或其他情況, 則轉到清單畫面.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitUpdate(T0010 t1, string btnSubmit)
        {
            // 執行修改. 
            // 要使用(自動檢核機制 + jQuery.Valicaion.unobtrusive), 就必須把參數交由ModelBinder處理, 否則無效.
            int mi1, mi2;
            switch (btnSubmit)
            {
                case "修改":
                    // 無論是MoldelBinder或是商業邏輯的錯誤, 都收集到ModelState中, 再交由VIEW顯示.
                    if (ModelState.IsValid)
                    {
                        P0030 oP0030 = new P0030();
                        if (!oP0030.Update(t1)) // 商業邏輯
                            if (string.IsNullOrEmpty(oP0030.msError))
                                ModelState.AddModelError("P0030", "修改失敗, 請聯絡系統管理人員取得失敗原因.");
                            else
                                ModelState.AddModelError("P0030", oP0030.msError);
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
                        P0030ViewModel vm1 = new P0030().ConvertModelToViewModel(t1);

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
                        P0030ViewModel vm1 = new P0030().ConvertModelToViewModel(new T0010().Read1Record(sKey1));

                        return View("Delete", vm1);
                    }
                    break;
            }
            return RedirectToAction("Index"); // 修改或刪除成功後或其他情況, 則轉到清單畫面.
        }

    }
}