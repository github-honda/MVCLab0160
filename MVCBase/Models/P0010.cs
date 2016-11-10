using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCBase.ViewModels;

namespace MVCBase.Models
{
    public class P0010
    {
        public P0010ViewModel BusinessTestView2(string id)
        {
            P0010ViewModel vm1 = new P0010ViewModel();
            T0010 t1 = new T0010().Read1Record(id);
            vm1.ms1 = t1.ms1; // 學號
            vm1.ms2 = t1.ms2; // 姓名
            vm1.mi1 = t1.mi1; // 國文分數, 同vm1.mi1 = t1.mi1 == null ? default(int) : t1.mi1;
            vm1.mi2 = t1.mi2; // 英文分數
            vm1.miSum = vm1.mi1 + vm1.mi2; // 計算總分
            vm1.mi1Extra = vm1.miSum / 2;  // 計算平均分數
            if ((vm1.mi1Extra) < 60) 
                vm1.msColor = "red"; // 平均低於60分的話, 以紅色顯示
            else
                vm1.msColor = "green";

            return vm1;
        }
        public P0010ListViewModel BusinessTestView()
        {
            P0010ListViewModel vm1 = new P0010ListViewModel();
            List<T0010> listT1 = new T0010().ReadList(); // 來自資料庫的清單
            List<P0010ViewModel> listBrowse1 = new List<P0010ViewModel>(); // 顯示在View上的清單
            foreach (T0010 t1 in listT1)
            {
                P0010ViewModel row1 = new P0010ViewModel();
                row1.ms1 = t1.ms1; // 學號
                row1.ms2 = t1.ms2; // 姓名
                row1.mi1 = t1.mi1; // 國文分數
                row1.mi2 = t1.mi2; // 英文分數
                row1.miSum = row1.mi1 + row1.mi2; // 計算總分
                row1.mi1Extra = row1.miSum / 2; // 計算平均分數
                if ((row1.mi1Extra) < 60)
                    row1.msColor = "red"; // 平均低於60分的話, 以紅色顯示
                else
                    row1.msColor = "green";

                listBrowse1.Add(row1);
            }
            vm1.msName = "3年2班";
            vm1.mList = listBrowse1;
            return vm1;
        }
    }
}

