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
            lock (_lock)  // Ensure thread safety
            {
                if (_instance == null)
                {
                    if (!File.Exists(configFilePath))
                        throw new FileNotFoundException("Configuration file not found");

                    var serializer = new GenericSerializer<DatabaseConnectionParameters>();
                    DatabaseConnectionParameters dbConfig = serializer.Deserialize(configFilePath);
                  

                    if (dbConfig == null)
                        throw new Exception("Failed to deserialize database configuration");

                    string connectionString = $"Server={dbConfig.ServerName};Database={dbConfig.DatabaseName};User Id={dbConfig.UserName};Password={dbConfig.Password};";
                    _instance = new SqlDatabaseManager(connectionString);
                }
                return _instance;
            }
        }
        private bool BindParameters(SqlCommand command, List<SqlParameter> parameters)
        {
            try
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataTable ExecuteSelectQuery(string sql, List<SqlParameter> parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    if (!BindParameters(command, parameters))
                        throw new Exception("Error binding parameters");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing SELECT query: {ex.Message}");
            }
            return dataTable;
        }
        private int ExecuteNonQuery(string sql, List<SqlParameter> parameters)
        {
            int affectedRows = 0;
            try
            {
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    if (!BindParameters(command, parameters))
                        throw new Exception("Error binding parameters");

                    _connection.Open();
                    affectedRows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing non-query: {ex.Message}");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return affectedRows;
        }

        public int ExecuteInsertQuery(string sql, List<SqlParameter> parameters)
        {
            return ExecuteNonQuery(sql, parameters);
        }

        public int ExecuteUpdateQuery(string sql, List<SqlParameter> parameters)
        {
            return ExecuteNonQuery(sql, parameters);
        }

        public int ExecuteDeleteQuery(string sql, List<SqlParameter> parameters)
        {
            return ExecuteNonQuery(sql, parameters);
        }
        public DateTime GetSqlServerDate()
        {
            DateTime serverDate = DateTime.MinValue;
            try
            {
                string sql = "SELECT GETDATE()";
                using (SqlCommand command = new SqlCommand(sql, _connection))
                {
                    _connection.Open();
                    serverDate = (DateTime)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving server date: {ex.Message}");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return serverDate;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection?.Dispose();
                    _connection = null;
                }

                _disposed = true;
            }
        }

        ~SqlDatabaseManager()
        {
            Dispose(false);
        }
    }
}
