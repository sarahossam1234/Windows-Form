using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Core.Framework.DataBase;

namespace Core.Framework.DataBase
{
    public class SqlDatabaseManager : IDisposable
    {
        private static SqlDatabaseManager _instance = null;
        private static readonly object _lock = new object();
        private SqlConnection _connection;
        private bool _disposed = false;

        private SqlDatabaseManager(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public static SqlDatabaseManager GetInstanceFromFile(string configFilePath)
        {
            lock (_lock) 
            {
                if (_instance == null)
                {
                    if (!File.Exists(configFilePath))
                        throw new FileNotFoundException("Configuration file not found");

                    
                    byte[] key = Convert.FromBase64String("your-base64-key-here");  
                    byte[] iv = Convert.FromBase64String("your-base64-iv-here");    

                    
                    var serializer = new GenericSerializer<DatabaseConnectionParameters>(key, iv);
                    DatabaseConnectionParameters dbConfig = serializer.Deserialize(configFilePath);

                    if (dbConfig == null)
                        throw new Exception("Failed to deserialize database configuration");

                    
                    string connectionString = $"Server={dbConfig.ServerName};Database={dbConfig.DatabaseName};User Id={dbConfig.UserName};Password={dbConfig.Password};";
                    _instance = new SqlDatabaseManager(connectionString);
                }
                return _instance;
            }
        }

        private void BindParameters(SqlCommand command, List<SqlParameter> parameters)
        {
            if (parameters == null) return;

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
                
