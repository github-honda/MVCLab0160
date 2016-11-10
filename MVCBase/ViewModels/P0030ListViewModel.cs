using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBase.ViewModels
{
    public class P0030ListViewModel
    {
        public string msName { get; set; }  // 班級
        public List<P0030ViewModel> mList { get; set; } // 學生清單
    }
}

