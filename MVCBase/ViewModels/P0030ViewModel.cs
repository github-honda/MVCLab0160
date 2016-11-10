using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.ComponentModel.DataAnnotations;
using System.ComponentModel; // for [DisplayName]


namespace MVCBase.ViewModels
{
    public class P0030ViewModel
    {
        [DisplayName("編號")]
        [Required(ErrorMessage = "不可空白.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "欄位[編號]長度必須為4碼.")]
        public string ms1 { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "不可空白.")]
        public string ms2 { get; set; } 

        [DisplayName("國文")]
        [Required(ErrorMessage = "不可空白.")]
        [Range(0, 100, ErrorMessage = "分數應在0到100分之間.")]
        public int mi1 { get; set; }   

        [DisplayName("英文")]
        [Required(ErrorMessage = "不可空白.")]
        [Range(0, 100, ErrorMessage = "分數應在0到100分之間.")]
        public int mi2 { get; set; }    

        [DisplayName("總分")]
        public int miSum { get; set; }  


        [DisplayName("平均")]
        public int mi1Extra { get; set; }

        public string msColor { get; set; } // 若平均低於60分, 則為紅色, 否則為綠色.

    }
}
