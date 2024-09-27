using System;
using System.IO;
using System.Xml.Serialization;

namespace Core.Framework.DataBase
{
    public class GenericSerializer<T>
    {
        public T Deserialize(string configFilePath)
        {
            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("Configuration file not found", configFilePath);
            }

            using (StreamReader reader = new StreamReader(configFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        public void SaveObjectData(string filePath, T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(writer, obj);
            }
        }
    }
}
