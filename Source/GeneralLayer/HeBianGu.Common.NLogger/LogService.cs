using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.NLogger
{ 
    public class LogService
    {
        public Logger _error_logger = LogManager.GetLogger("api-log-error");

        public Logger _logger = LogManager.GetLogger("api-log-info");


        public void Trace(params string[] messages)
        {
            foreach (var message in messages)
            {
                _logger.Trace(message);
            }
        }

        public void Debug(params string[] messages)
        {
            foreach (var message in messages)
            {
                _logger.Debug(message);
            }
        }

        public void Info(params string[] messages)
        {
            foreach (var message in messages)
            {
                _logger.Info(message);
            }
        }

        public void Warn(params string[] messages)
        {
            foreach (var message in messages)
            {
                _error_logger.Warn(message);
            }
        }

        public void Error(params string[] messages)
        {
            foreach (var message in messages)
            {
                _error_logger.Error(message);
            }
        }

        public void Error(params Exception[] messages)
        {
            foreach (var message in messages)
            {
                _error_logger.Error(message);
            }
        }

        public void Fatal(params string[] messages)
        {
            foreach (var message in messages)
            {
                _error_logger.Fatal(message);
            }
        }

        public void Fatal(params Exception[] messages)
        {
            foreach (var message in messages)
            {
                _error_logger.Error(message);
            }
        }


    }
}
