using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    public class DBLogService : IDBLogService
    {
        IDebugRespository _debug;
        IErrorRespository _error;
        IFatalRespository _fatal;
        IInfoRespository _info;
        IWarnRespository _warn;

        public DBLogService(IDebugRespository debug, IErrorRespository error, IFatalRespository fatal, IInfoRespository info, IWarnRespository warn)
        {
            this._debug = debug;
            this._error = error;
            this._fatal = fatal;
            this._info = info;
            this._warn = warn;
        }
        public void LogDebug(string message, string value = null, string title = null)
        {
            hl_dm_debug model = new hl_dm_debug();

            model.Title = title;

            model.Message = message;

            model.Value = value;

            this._debug.InsertAsync(model);
        }

        public void LogError(string message, Exception ex = null, string title = null)
        {
            hl_dm_error model = new hl_dm_error();

            model.Message = message;

            model.Exception = ex?.ToString();

            StackTrace trace = new StackTrace();

            model.Stack = trace.ToString();

            this._error.InsertAsync(model);
        }

        public void LogFatal(string message, Exception ex = null, string title = null)
        {
            hl_dm_fatal model = new hl_dm_fatal();

            model.Message = message;

            model.Exception = ex?.ToString();

            StackTrace trace = new StackTrace();

            model.Stack = trace.ToString();

            var process = Process.GetCurrentProcess();

            var ps = process.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();

            foreach (var p in ps)
            {
                if (!p.CanRead) continue;

                var ba = p.GetCustomAttribute<BrowsableAttribute>();

                if (ba != null && ba.Browsable == false) continue;

                sb.AppendLine($"{p.Name} : {p.GetValue(process)}");
            }

            model.Process = sb.ToString();

            this._fatal.InsertAsync(model);
        }


        public void LogInfo(string message, string title = null)
        {
            hl_dm_info model = new hl_dm_info();

            model.Title = title;

            model.Message = message;

            this._info.InsertAsync(model);
        } 

        public void LogWarn(string message, string title = null)
        {
            hl_dm_warn model = new hl_dm_warn();

            model.Title = title;

            model.Message = message;

            this._warn.InsertAsync(model);
        }
    }

    public interface IDBLogService
    {
        void LogInfo(string message, string title = null);

        void LogDebug(string message, string value = null, string title = null);

        void LogError(string message, Exception ex = null, string title = null);

        void LogWarn(string message, string title = null);

        void LogFatal(string message, Exception ex = null, string title = null);
    }
}
