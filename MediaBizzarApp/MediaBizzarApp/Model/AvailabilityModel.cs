using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MediaBizzarApp
{
    class AvailabilityModel
    {
        private SQLConnection sqlConnection;
        private string connectionString;
        private string availabilityTable;
        private string[] Key = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        public AvailabilityModel()
        {
            sqlConnection = new SQLConnection();
            connectionString = sqlConnection.ConnectionString;
            availabilityTable = "employee_availability";
        }
        public Dictionary<string, int> Get(int employeeID)
        {
            try {
                using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                    string sql = $"SELECT Mon, Tue, Wed, Thu, Fri, Sat, Sun FROM {availabilityTable} WHERE ID = @id;";

                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@id", employeeID);

                    connection.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    Dictionary<string, int> result = new Dictionary<string, int>();
                    int length = Key.Length;

                    for (int i = 0; i < length; i++)
                    {
                        result[Key[i]] = 7;
                    }

                    while (dr.Read()) {
                        for(int i = 0; i < length; i++) {
                            result[Key[i]] = Convert.ToInt32(dr[i]);
                        }
                    }
                    connection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
