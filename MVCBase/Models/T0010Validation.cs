using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.ComponentModel.DataAnnotations;

namespace MVCBase.Models
{
    /// <summary>
    /// 示範自訂DataAnnotations屬性
    /// </summary>
    public class T0010Validation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int i1 = 0;
            // 欄位未輸入, 或是型態不符(例如: int type輸入"XXX"字串, 就會以null傳進來.
            if (value == null) 
                return new ValidationResult(string.Format("請輸入欄位[{0}]", validationContext.DisplayName));

            // 型別轉換在ModelBinder中已先處理, 不會有機會跑到此處可以檢查Data Type, 除非原來是宣告為int? 或 string
            if (!int.TryParse(value.ToString(), out i1))
                return new ValidationResult(string.Format("分數欄位[{0}]請輸入數字.", validationContext.DisplayName));

            if (i1<0 || i1>100)
                return new ValidationResult(string.Format("分數欄位[{0}]應在0到100分之間.", validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}


