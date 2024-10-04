using Exception_Log;
using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            
            ExceptionLogFactory logFactory = new ExceptionLogFactory(LogType.File, "error.log");
            logFactory.LogException(new Exception("Sample exception"));

            string randomString = ExtensionHelper.GenerateRandomString(RandomType.AlphaNumeric, 10);
            Console.WriteLine("Generated Random String: " + randomString);

            DateTime date = ExtensionHelper.ConvertToDate("05/10/2024");
            Console.WriteLine("Converted Date: " + date);

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}

