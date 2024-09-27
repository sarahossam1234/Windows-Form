using Core.Framework.DataBase;
using System;
using System.IO;

namespace FirstProject
{
    internal class Database
    {
        private DatabaseConnectionParameters _dbParams;

        public Database()
        {
        }
        internal string GetConnectionString()
        {
            if (_dbParams == null)
            {
                throw new InvalidOperationException("Database connection parameters are not loaded.");
            }
            return _dbParams.GetConnectionString();
        }

        internal void LoadConnectionString(string filePath)
        {
           
            GenericSerializer<DatabaseConnectionParameters> serializer = new GenericSerializer<DatabaseConnectionParameters>();
            _dbParams = serializer.Deserialize(filePath);
        }

        internal void SaveConnectionString(string filePath, DatabaseConnectionParameters dbParams)
        {
            GenericSerializer<DatabaseConnectionParameters> serializer = new GenericSerializer<DatabaseConnectionParameters>();
            serializer.SaveObjectData(filePath, dbParams);
        }
    }
}
