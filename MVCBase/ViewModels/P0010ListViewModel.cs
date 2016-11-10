using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCBase.Models;

namespace MVCBase.ViewModels
{
    public class P0010ListViewModel
    {
        public string msName { get; set; }  // 班級
        public List<P0010ViewModel> mList { get; set; } // 學生清單
    }
}

