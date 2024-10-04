using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Log
{
    public enum LogType
    {
        File,
        Email
    }

    public class ExceptionLogFactory
    {
        private IExceptionLog _logger;

        public ExceptionLogFactory(LogType logType, string logTarget)
        {
            switch (logType)
            {
                case LogType.File:
                    _logger = new FileExceptionLogger(logTarget);
                    break;
                case LogType.Email:
                    _logger = new EmailExceptionLogger(logTarget);
                    break;
                default:
                    throw new ArgumentException("Invalid log type.");
            }
        }

        public string LogException(Exception exception)
        {
            try
            {
                return _logger.LogException(exception);
            }
            catch (Exception ex)
            {
                return $"An error occurred while logging: {ex.Message}";
            }
        }
    }

}
