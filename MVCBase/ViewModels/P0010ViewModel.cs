using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBase.ViewModels
{
    public class P0010ViewModel
    {
        public string ms1 { get; set; } // 編號
        public string ms2 { get; set; } // 姓名
        public int mi1 { get; set; }    // 國文
        public int mi2 { get; set; }    // 英文

        public int miSum { get; set; }  // 總分
        public int mi1Extra { get; set; } // 平均
        public string msColor { get; set; } // 若平均低於60分, 則為紅色, 否則為綠色.
    }
}
