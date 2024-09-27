using System;
using System.IO;
using System.Xml.Serialization;

namespace Core.Framework.DataBase
{
    internal class GenericSerializer
    {
        internal T ReadObjectData(string configFilePath) =>
            
            Deserialize(configFilePath);

        private T Deserialize(string configFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
