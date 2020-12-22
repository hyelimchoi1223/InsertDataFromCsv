using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace InsertDataFromCsv
{
    internal class DBHelper
    {
        string connstring = ConfigurationManager.ConnectionStrings["MariaDB"].ConnectionString;

        public bool ExecuteNonQuery(string query)
        {
            bool isSuccess = false;

            using (MySqlConnection con = new MySqlConnection(connstring))
            {
                con.Open();
                con.Execute(query.ToString());
                con.Close();
                isSuccess = true;
            }            
            return isSuccess;
        }
    }
}