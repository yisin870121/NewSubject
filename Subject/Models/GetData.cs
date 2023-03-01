using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Subject.Models
{
    public class GetData
    {
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SpecialSubjectConnection"].ConnectionString);

        SqlCommand cmd = new SqlCommand();

        SqlDataReader rd;

        SqlDataAdapter adp = new SqlDataAdapter("", conn);

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        public DataTable TableQuery(string sql)
        {
            adp.SelectCommand.CommandText = sql;  //指定 Select Command
            adp.Fill(ds);  //把取道的Table填入DataSet

            dt = ds.Tables[0];

            return dt;
        }

        public DataTable TableQuery(string sql, List<SqlParameter> para)
        {
            adp.SelectCommand.CommandText = sql;  //指定 Select Command

            foreach (SqlParameter p in para)
            {
                adp.SelectCommand.Parameters.Add(p);
            }

            adp.Fill(ds);  //把取道的Table填入DataSet

            dt = ds.Tables[0];

            return dt;
        }


        //public SqlDataReader LoginQuery(string sql, List<SqlParameter> para)
        //{
        //    cmd.CommandText = sql;

        //    foreach (SqlParameter p in para)
        //    {
        //        cmd.Parameters.Add(p);
        //    }

        //    conn.Open();
        //    try
        //    {
        //        rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        rd.Read();
        //    }
        //    catch
        //    {
        //        conn.Close();
        //        return rd;
        //    }

        //    return rd;
        //}
    }
}