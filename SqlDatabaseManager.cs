using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Core.Framework.DataBase
{
    public class SqlDatabaseManager
    {
        private static SqlDatabaseManager _instance;
        private static readonly object _lock = new object();
        private SqlConnection _connection;

        private SqlDatabaseManager(string server, string database, string username, string password)
        {
            var connectionString = $"Server={server};Database={database};User Id={username};Password={password};";
            _connection = new SqlConnection(connectionString);
            _connection.Open(); 
        }

        public static SqlDatabaseManager GetInstance(string server, string database, string username, string password)
        {
            if (_instance == null)
            {
                lock (_lock)  
                {
                    if (_instance == null)
                    {
                        _instance = new SqlDatabaseManager(server, database, username, password);
                    }
                }
            }
            return _instance;
        }

        public void ExecuteSelectQuery(string query, List<SqlParameter> parameters)
        {
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                    }
                }
            }
        }

      
        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
