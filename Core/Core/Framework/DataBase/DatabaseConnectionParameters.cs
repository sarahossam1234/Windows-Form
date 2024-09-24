using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class DatabaseConnectionParameters
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public DatabaseConnectionParameters() { }

        public DatabaseConnectionParameters(string serverName, string databaseName, string userName, string password)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
            UserName = userName;
            Password = password;
        }

        // Returns a formatted connection string
        public string GetConnectionString()
        {
            return $"Server={ServerName};Database={DatabaseName};User Id={UserName};Password={Password};";
        }
    }
}
