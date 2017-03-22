using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace LILI_BMS.Service
{
    public class ApplicationLogger
    {
        private static ApplicationLogger uniqeInstance;
        private static LogEntry _log;// = null;

        private ApplicationLogger()
        {

        }
        public static ApplicationLogger getInstance(string category)
        {
            if (uniqeInstance == null)
            {
                _log = new LogEntry();
                _log.Categories.Add(category);
                uniqeInstance = new ApplicationLogger();
            }
            return uniqeInstance;
        }

        public void WriteLog(string msg)
        {
            _log.Message = msg;
            Logger.Write(_log);
        }
    }
}
