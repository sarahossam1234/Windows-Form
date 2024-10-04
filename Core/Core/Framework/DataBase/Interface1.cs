using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_Log
{
    public interface IExceptionLog
    {
        string LogException(Exception exception);
    }

}
