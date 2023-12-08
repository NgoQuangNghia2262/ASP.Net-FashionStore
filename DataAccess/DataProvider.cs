using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataProvider
    {
        private readonly string str = @"Data Source=DESKTOP-BOC9JRS\SQLEXPRESS;Initial Catalog=FashionStore;Integrated Security=True";
        private static DataProvider? instance;
        public static DataProvider Instance
        {
            get { if (instance == null) { instance = new DataProvider(); } return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }
        public DataTable ExecuteQuery(string query)
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
        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conne = new SqlConnection(str))
            {
                conne.Open();
                SqlCommand cmd = new SqlCommand(query, conne);
                SqlDataReader record = await cmd.ExecuteReaderAsync();
                dt.Load(record);
                conne.Close();
            }
            return dt;
        }
        async public Task<int> ExecuteNonQueryAsync(string query)
        {
            int dt = 0;
            using (SqlConnection conne = new SqlConnection(str))
            {
                conne.Open();
                SqlCommand cmd = new SqlCommand(query, conne);
                dt = await cmd.ExecuteNonQueryAsync();
                conne.Close();
            }
            return dt;
        }
        public Task<object> ExecuteScalarAsync(string query)
        {
            Task<object> dt;
            using (SqlConnection conne = new SqlConnection(str))
            {
                conne.Open();
                SqlCommand cmd = new SqlCommand(query, conne);
#pragma warning disable CS8619 
                dt = cmd.ExecuteScalarAsync();
#pragma warning restore CS8619 
                conne.Close();
            }
            return dt;
        }
    }
}
