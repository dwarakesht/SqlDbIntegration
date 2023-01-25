using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLClientLibrary
{
    public class SqlClient1
    {
        static SqlConnection conn = null;
        static SqlClient1 client = null;
        string connectionString = @"Data Source=DESKTOP-LL79508\SQLEXPRESS1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private SqlClient1()
        {

        }
        //Get Connection
        public SqlConnection GetConnection()
        {
            if(conn == null)
            {
                conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                }
                catch(Exception)
                {
                    throw new Exception("Connection could not be established.");
                }
            }
            return conn;
        }

        public static SqlClient1 GetInstance()
        {
            if (client == null)
            {
                client = new SqlClient1();
            }
            return client;
        }
        //Get Query Result
        public SqlDataReader GetSqlDataReader(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    query, conn))
                {
                    return cmd.ExecuteReader();
                }
            }
        }

        public DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        //Validate Result
        public  bool validateQueryResult(SqlDataReader dr, string columnName, string value)
        {
            while(dr.Read())
            {
                string name = dr.GetValue(dr.GetOrdinal(columnName)).ToString();
                if (name.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public bool validateQueryResultDataSet(DataSet ds, string columnName, string value)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string name = ds.Tables[0].Rows[i][columnName].ToString();
                if (name.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
