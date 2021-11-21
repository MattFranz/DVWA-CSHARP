using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace OWASP10_2021.Data
{

    public class DAL
    {
        public SqlConnection connection;

        public DAL(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        private SqlDataReader ExecuteDataReader(string SQL)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = SQL;
            return cmd.ExecuteReader(); 
        }

        public void ExecuteSQL(string SQL)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = SQL;
            cmd.ExecuteNonQuery();
        }

        public DataTable GetData(string SQL)
        {
            connection.Open();
            var rdr = ExecuteDataReader(SQL);
            var dt = new DataTable();

            dt.Load(rdr);
            return dt;
        }
    }
}
