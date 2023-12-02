using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class DataProvider
    {
        private readonly string str = @"Data Source=DESKTOP-BOC9JRS\SQLEXPRESS;Initial Catalog=FashionStore;Integrated Security=True";
        private static DataProvider? instance;
        public static DataProvider Instance
        {
            get { if (instance == null) { instance = new DataProvider(); } return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }
        public DataTable ExecuteQuery(string query )
        {
            DataTable dt = new DataTable();
            using (SqlConnection conne = new SqlConnection(str))
            {
                conne.Open();
                SqlCommand cmd = new SqlCommand(query, conne);
               
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
               
                conne.Close();
            }
            return dt;
        }
        public int ExecuteNonQuery(string query )
        {
            int dt = 0;
            using (SqlConnection conne = new SqlConnection(str))
            {
                conne.Open();
                SqlCommand cmd = new SqlCommand(query, conne);
                dt = cmd.ExecuteNonQuery();
                conne.Close();
            }
            return dt;
        }
        public object ExecutesScalar(string query )
        {
            object dt = 0;
            using (SqlConnection conne = new SqlConnection(str))
            {
                conne.Open();
                SqlCommand cmd = new SqlCommand(query, conne);
                dt = cmd.ExecuteScalar();
                conne.Close();
            }
            return dt;
        }
    }
}
