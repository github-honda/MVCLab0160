using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.Text;
using System.Web.Mvc;
using System.Diagnostics;

namespace MVCBase.Models
{

    /// <summary>
    /// Project common functions
    /// </summary>
    public class CMN0010
    {
        /// <summary>
        /// 示範取得ModelState清單. 這個功能等於@Html.ValidationSummary(false, "", new { style = "color:red;" })
        /// </summary>
        /// <param name="state1"></param>
        /// <returns></returns>
        public string GetModelStateErrorList(ModelStateDictionary state1)
        {
            // 示範取得ModelState清單. 這個功能等於@Html.ValidationSummary(false, "", new { style = "color:red;" })
            StringBuilder sb1 = new StringBuilder();
            sb1.Length = 0;
            foreach (var m1 in state1)
            {
                if (!state1.IsValidField(m1.Key))
                {
                    foreach (var err1 in m1.Value.Errors)
                    {
                        sb1.AppendFormat("<br />{0}, Error={1}", m1.Key, err1.ErrorMessage);
                    }
                }
            }
            if (sb1.Length > 0)
                return string.Format("<br /><br /><div style='color: red;'>ModelState中的錯誤清單:{0}</div>", sb1.ToString());
            else
                return sb1.ToString();
        }
    }
}