using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using MVCBase.ViewModels;

namespace MVCBase.Models
{
    public class P0030
    {
        public string msError { get; private set; } // 執行錯誤, 或是檢核錯誤時的訊息.
        public int miAffected { get; private set; } // 執行結果影響的資料筆數.
        void ClearBeforeAction()
        {
            miAffected = 0;
            msError = string.Empty;
        }

        public bool Create(T0010 t1)
        {
            // 新增
            ClearBeforeAction();
            if ((t1.mi1 < 30) && (t1.mi2 < 30))
            {
                msError = "國文跟英文不可能都低於30分";
                return false;
            }
            miAffected = new T0010().Create(t1);
            if (miAffected < 1) return false;
            return true;
        }
        public bool Update(T0010 t1)
        {
            // 修改
            ClearBeforeAction();
            if ((t1.mi1 < 30) && (t1.mi2 < 30))
            {
                msError = "國文跟英文不可能都低於30分";
                return false;
            }
            miAffected = new T0010().Update(t1);
            if (miAffected < 1) return false;
            return true;
        }
        public bool Delete(string id)
        {
            // 刪除
            ClearBeforeAction();
            if (id.StartsWith("0"))
            {
                msError = "編號0字頭的測試資料不可以刪除.";
                return false;
            }
            miAffected = new T0010().Delete(id);
            if (miAffected < 1) return false;
            return true;
        }
        public P0030ViewModel Read(string id)
        {
            // 單筆顯示
            T0010 t1 = new T0010().Read1Record(id);
            P0030ViewModel vm1 = ConvertModelToViewModel(t1);
            return vm1;
        }
        public P0030ListViewModel Index()
        {
            // 多筆清單顯示
            P0030ListViewModel vm1 = new P0030ListViewModel();
            List<T0010> listT1 = new T0010().ReadList(); // 來自資料庫的清單
            List<P0030ViewModel> listBrowse1 = new List<P0030ViewModel>(); // 顯示在View上的清單
            foreach (T0010 t1 in listT1)
            {
                P0030ViewModel row1 = ConvertModelToViewModel(t1);
                listBrowse1.Add(row1);
            }
            vm1.msName = "3年2班";
            vm1.mList = listBrowse1;
            return vm1;
        }
        public P0030ViewModel ConvertModelToViewModel(T0010 t1)
        {
            // 從資料model轉為ViewModel都是同樣的邏輯, 可以轉為公用函數.
            P0030ViewModel vm1 = new P0030ViewModel();
            vm1.ms1 = t1.ms1; // 學號
            vm1.ms2 = t1.ms2; // 姓名
            vm1.mi1 = t1.mi1; // 國文分數
            vm1.mi2 = t1.mi2; // 英文分數
            vm1.miSum = vm1.mi1 + vm1.mi2; // 計算總分
            vm1.mi1Extra = vm1.miSum / 2;  // 計算平均分數
            if ((vm1.mi1Extra) < 60)
                vm1.msColor = "red"; // 平均低於60分的話, 以紅色顯示
            else
                vm1.msColor = "green";
            return vm1;
        }
    }
}