using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp7
{
    public class GenericSerializer<T>
    {
        private readonly byte[] key = Encoding.UTF8.GetBytes("1234567890123456");
        private readonly byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");

        public void SaveObjectData(string filePath, T obj)
        {
            string xmlString = SerializeToXml(obj);
            string encryptedData = EncryptString(xmlString);
            File.WriteAllText(filePath, encryptedData);
        }

        public T ReadObjectData(string filePath)
        {
            string encryptedData = File.ReadAllText(filePath);
            string decryptedXml = DecryptString(encryptedData);
            return DeserializeFromXml(decryptedXml);
        }

        private string SerializeToXml(T obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        private T DeserializeFromXml(string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(xmlString))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

        private string EncryptString(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        byte[] encrypted = ms.ToArray();
                        return Convert.ToBase64String(encrypted);
                    }
                }
            }
        }

        private string DecryptString(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

    
