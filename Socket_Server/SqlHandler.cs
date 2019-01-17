using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Socket_Server
{
    public class SqlHandler
    {
        string connString = ConfigurationManager.ConnectionStrings["DBInfo"].ConnectionString;

        public SqlHandler()
        {

        }

        public string executeNonQuery(string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 600;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "OK";
        }
    }
}
