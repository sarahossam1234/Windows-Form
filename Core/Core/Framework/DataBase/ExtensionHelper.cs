using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Log
{
    public enum RandomType
    {
        Alphabet,
        Numeric,
        AlphaNumeric
    }

    public class ExtensionHelper
    {
        public static string GenerateRandomString(RandomType randomType, int length)
        {
            try
            {
                const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                const string numbers = "0123456789";
                const string alphaNumeric = alphabet + numbers;

                string chars = randomType switch
                {
                    RandomType.Alphabet => alphabet,
                    RandomType.Numeric => numbers,
                    RandomType.AlphaNumeric => alphaNumeric,
                    _ => throw new ArgumentException("Invalid random type.")
                };

                StringBuilder result = new StringBuilder();
                Random random = new Random();
                for (int i = 0; i < length; i++)
                {
                    result.Append(chars[random.Next(chars.Length)]);
                }

                return result.ToString();
            }
            catch (Exception ex)
            {
                new ExceptionLogFactory(LogType.File, "error.log").LogException(ex);
                throw;
            }
        }

        public static string Digitize(string input)
        {
            try
            {
                return new string(input.Where(char.IsDigit).ToArray());
            }
            catch (Exception ex)
            {
                new ExceptionLogFactory(LogType.File, "error.log").LogException(ex);
                throw;
            }
        }

        public static DateTime ConvertToDate(string dateString)
        {
            try
            {
                return DateTime.ParseExact(dateString, "dd/MM/yyyy", null);
            }
            catch (Exception ex)
            {
                new ExceptionLogFactory(LogType.File, "error.log").LogException(ex);
                throw new FormatException("Invalid date format.");
            }
        }

        public static DateTime ConvertToLongDate(string dateString)
        {
            try
            {
                return DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm", null);
            }
            catch (Exception ex)
            {
                new ExceptionLogFactory(LogType.File, "error.log").LogException(ex);
                throw new FormatException("Invalid date-time format.");
            }
        }
    }
}
