using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Core.Framework.DataBase
{
    public class GenericSerializer<T>
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        
        public GenericSerializer(byte[] key, byte[] iv)
        {
            this.key = key;
            this.iv = iv;
        }

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

        
        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
