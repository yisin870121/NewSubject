using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Subject.Models
{
    public class SetData
    {
        //1.建立資料庫連線物件
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SpecialSubjectConnection"].ConnectionString);
        //2.建立SQL命令物件        
        SqlCommand cmd = new SqlCommand("", conn);

        public void executeSql(string sql)
        {
            cmd.CommandText = sql;

            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }

        public void executeSql(string sql, List<SqlParameter> list)
        {
            cmd.CommandText = sql;

            foreach (var p in list)
            {
                if (p.SqlValue == null)
                {
                    p.SqlValue = DBNull.Value;
                }
                
                cmd.Parameters.Add(p);
               
            }

            //foreach (var p in list)
            //{
            //    cmd.Parameters.Add(p);
            //}

            conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();

        }

        








    }
}