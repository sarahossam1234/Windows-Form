using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Log
{
    public class FileExceptionLogger : IExceptionLog
    {
        private readonly string _filePath;

        public FileExceptionLogger(string filePath)
        {
            _filePath = filePath;
        }

        public string LogException(Exception exception)
        {
            try
            {
                string exceptionDetails = $"Exception: {exception.Message}\nStack Trace: {exception.StackTrace}";
                File.AppendAllText(_filePath, exceptionDetails);
                return "An error occurred. Please check the log file for details.";
            }
            catch (Exception ex)
            {
                return $"An error occurred while logging the exception: {ex.Message}";
            }
        }
    }
}
