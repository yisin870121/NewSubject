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
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SpecialSubject"].ConnectionString);

        SqlCommand cmd = new SqlCommand();

        SqlDataReader rd;

        public SqlDataReader LoginQuery(string sql, List<SqlParameter> para)
        {
            cmd.CommandText = sql;

            foreach (SqlParameter p in para)
            {
                cmd.Parameters.Add(p);
            }

            conn.Open();
            try
            {
                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                rd.Read();
            }
            catch
            {
                conn.Close();
                return rd;
            }

            return rd;
        }
    }
}