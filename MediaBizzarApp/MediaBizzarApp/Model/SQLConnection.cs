using System;
using System.Collections.Generic;
using System.Text;

namespace MediaBizzarApp
{
    sealed class SQLConnection
    {
        private string server;
        private string database;
        private string username;
        private string password;
        public string ConnectionString { get; private set; }

        public SQLConnection()
        {
            server = "studmysql01.fhict.local";
            database = "dbi429506";
            username = "dbi429506";
            password = "bangbang56";
            ConnectionString = $"server={server};" +
                               $"database={database};" +
                               $"uid={username};" +
                               $"password={password};";
        }
    }
}
