namespace Core.Framework.DataBase
{
    public class DatabaseConnectionParameters
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; 
        public DatabaseConnectionParameters() { }

        public DatabaseConnectionParameters(string serverName, string databaseName, string userName, string password)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
            UserName = userName;
            Password = password;
        }
        public string GetConnectionString()
        {
            return $"Server={ServerName};Database={DatabaseName};User Id={UserName};Password={Password};";
        }
    }
}
