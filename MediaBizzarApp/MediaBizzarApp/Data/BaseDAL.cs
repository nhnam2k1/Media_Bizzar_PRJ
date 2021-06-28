using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace MediaBizzarApp
{
    public class BaseDAL
    {
        public MySqlConnection GetConnection()
        {
            MySqlConnection con = new MySqlConnection(@"Server=studmysql01.fhict.local; Uid=dbi429506; Database=dbi429506; Pwd=bangbang56 ;");

           

            // This is Nhat Nam part, please comment below if you want to use your part 
            /*string server = "studmysql01.fhict.local";
            string database = "dbi429506";
            string username = "dbi429506";
            string password = "bangbang56";
            string ConnectionString = $"server={server};" +
                               $"database={database};" +
                               $"uid={username};" +
                               $"password={password};";
            MySqlConnection con = new MySqlConnection(ConnectionString);*/
            return con;
        }

        public MySqlCommand defaultDatabaseConnection(string sql, string[] bindings = null)
        {
            MySqlConnection con = this.GetConnection();
            MySqlCommand cmd = con.CreateCommand();
            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;


                List<string> fields = new List<string>();
                MatchCollection mcol = Regex.Matches(sql, @"@\b\S+?\b");

                foreach (Match m in mcol)
                {
                    fields.Add(m.ToString());
                }

                if (bindings != null)
                {
                    for (int i = 0; i < bindings.Length; i++)
                    {
                        cmd.Parameters.Add(fields[i], MySqlDbType.VarChar).Value = bindings[i];
                    }
                }

                return cmd;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }


        public MySqlDataReader executeReader(string sql, string[] bindings = null)
        {
            return this.defaultDatabaseConnection(sql, bindings).ExecuteReader();

        }

        public Object executeScalar(string sql, string[] bindings = null)
        {
            return this.defaultDatabaseConnection(sql, bindings).ExecuteScalar();
        }

        public Object executeNonQuery(string sql, string[] bindings = null)
        {
            return this.defaultDatabaseConnection(sql, bindings).ExecuteNonQuery();
        }




        public void CloseConnection(MySqlDataReader con)
        {
            con.Close();
        }

    }
}
