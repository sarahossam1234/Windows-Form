using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class Database
    {
        private string _connectionString;
        private GenericSerializer<DatabaseConnectionParameters> _serializer;

        public Database()
        {
            _serializer = new GenericSerializer<DatabaseConnectionParameters>();
        }

        public void LoadConnectionString(string filePath)
        {
            if (!filePath.EndsWith(".STD"))
            {
                throw new InvalidOperationException("File must have a .STD extension.");
            }

            DatabaseConnectionParameters parameters = _serializer.ReadObjectData(filePath);
            _connectionString = parameters.GetConnectionString();
        }

        public void SaveConnectionString(string filePath, DatabaseConnectionParameters parameters)
        {
            if (!filePath.EndsWith(".STD"))
            {
                throw new InvalidOperationException("File must have a .STD extension.");
            }

            _serializer.SaveObjectData(filePath, parameters);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
