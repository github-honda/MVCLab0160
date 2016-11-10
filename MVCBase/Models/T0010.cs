using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel; // for [DisplayName]

namespace MVCBase.Models
{
    public class T0010
    {
        [DisplayName("編號")]
        [Required(ErrorMessage = "不可空白.")] 
        [StringLength(4, MinimumLength =4,  ErrorMessage = "欄位[編號]長度必須為4碼.")]
        public string ms1 { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "不可空白.")] 
        public string ms2 { get; set; }

        [DisplayName("國文")]
        [Range(0, 100, ErrorMessage = "國文分數應在0到100分之間.")]
        public int mi1 { get; set; } 

        [DisplayName("英文")]
        [T0010Validation] // 自訂檢核屬性. int type預設已包含(Required及Data type)
        public int mi2 { get; set; }

        public T0010 Read1Record(string sFS01)
        {
            T0010 t1 = new T0010();
            using (SqlConnection cnn1 = new SqlConnection(GetConnectionsString())) {
                cnn1.Open();
                using (SqlCommand cmd1 = new SqlCommand("select FS01, FS02, FI01, FI02 from T0010 where FS01=@FS01", cnn1)) {
                    cmd1.Parameters.Add("@FS01", SqlDbType.VarChar, 50).Value = sFS01; 
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        t1.ms1 = reader1.GetString(0);
                        t1.ms2 = reader1.GetString(1);
                        t1.mi1 = reader1.GetInt32(2);
                        t1.mi2 = reader1.GetInt32(3);
                    }
                }
            }
            return t1;
        }
        public List<T0010> ReadList()
        {
            List<T0010> oList = new List<T0010>();
            using (SqlConnection cnn1 = new SqlConnection(GetConnectionsString()))
            {
                cnn1.Open();
                using (SqlCommand cmd1 = new SqlCommand("select FS01, FS02, FI01, FI02 from T0010 order by FS01", cnn1))
                {
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        T0010 t1 = new T0010();
                        t1.ms1 = reader1.GetString(0);
                        t1.ms2 = reader1.GetString(1);
                        t1.mi1 = reader1.GetInt32(2);
                        t1.mi2 = reader1.GetInt32(3);
                        oList.Add(t1);
                    }
                }
            }
            return oList;
        }
        public int Create(T0010 t1)
        {
            int iAffected = 0;
            using (SqlConnection cnn1 = new SqlConnection(GetConnectionsString()))
            {
                cnn1.Open();
                using (SqlCommand cmd1 = new SqlCommand("insert into T0010 ([FS01], [FS02], [FI01], [FI02]) values (@FS01, @FS02, @FI01,@FI02)", cnn1))
                {
                    cmd1.Parameters.Add("@FS01", SqlDbType.VarChar, 50).Value = t1.ms1;
                    cmd1.Parameters.Add("@FS02", SqlDbType.NVarChar, 50).Value = t1.ms2;
                    cmd1.Parameters.Add("@FI01", SqlDbType.Int).Value = t1.mi1;
                    cmd1.Parameters.Add("@FI02", SqlDbType.Int).Value = t1.mi2;
                    iAffected = cmd1.ExecuteNonQuery();  
                }
            }
            return iAffected;
        }
        public int Update(T0010 t1)
        {
            int iAffected = 0;
            using (SqlConnection cnn1 = new SqlConnection(GetConnectionsString()))
            {
                cnn1.Open();
                using (SqlCommand cmd1 = new SqlCommand("update T0010 set [FS02]=@FS02, [FI01]=@FI01, [FI02]=@FI02 where [FS01]=@FS01", cnn1))
                {
                    cmd1.Parameters.Add("@FS01", SqlDbType.VarChar, 50).Value = t1.ms1;
                    cmd1.Parameters.Add("@FS02", SqlDbType.NVarChar, 50).Value = t1.ms2;
                    cmd1.Parameters.Add("@FI01", SqlDbType.Int).Value = t1.mi1;
                    cmd1.Parameters.Add("@FI02", SqlDbType.Int).Value = t1.mi2;
                    iAffected = cmd1.ExecuteNonQuery();
                }
            }
            return iAffected;
        }
        public int Delete(string sFS01)
        {
            int iAffected = 0;
            using (SqlConnection cnn1 = new SqlConnection(GetConnectionsString()))
            {
                cnn1.Open();
                using (SqlCommand cmd1 = new SqlCommand("delete from T0010 where [FS01]=@FS01", cnn1))
                {
                    cmd1.Parameters.Add("@FS01", SqlDbType.VarChar, 50).Value = sFS01;
                    iAffected = cmd1.ExecuteNonQuery();
                }
            }
            return iAffected;
        }
        string GetConnectionsString()
        {
            string sConnectionString = "請在Web.Config中設定name=LocalDB01的ConnectionString";
            Configuration configuration1 = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings setting1;
            if (configuration1.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                setting1 = configuration1.ConnectionStrings.ConnectionStrings["LocalDB01"];
                if (setting1 != null)
                    sConnectionString = setting1.ConnectionString;
            }
            return sConnectionString;
        }

    }
}


