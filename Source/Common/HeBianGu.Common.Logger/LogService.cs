using log4net;
using log4net.Appender;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.Logger
{
    /// <summary>
    /// 统一日志输出类
    /// </summary>
    public class LogService : ILogService
    {

        public LogService()
        {
            var process = Process.GetCurrentProcess().ProcessName;

            this.InitLogger(process);
        }
        /// <summary>
        /// 运行日志
        /// </summary>
        public void Info(params string[] message)
        {
            if (Logger == null) return;

            foreach (var item in message)
            {
                Logger.Info(message);
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        public void Error(params Exception[] ex)
        {
            if (Logger == null) return;

            foreach (var item in ex)
            {
                Logger.Error(ex);
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Error(string message, Exception ex)
        {
            Logger.Error(ex);
        }

        /// <summary>
        /// 初始化日志
        /// </summary>
        public void InitLogger(string name)
        {

            string logconfig = @"log4net.config";

            ReplaceFileTag(logconfig);

            Stopwatch st = new Stopwatch();
            //  开始计时
            st.Start();
            log4net.GlobalContext.Properties["dynamicName"] = name;
            Logger = LogManager.GetLogger(name);
            //  终止计时
            st.Stop();
            if (st.ElapsedMilliseconds > 2000)
            {
                Logger.Info("log4net.config file ERROR!!!");
                System.IO.FileStream fs = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                string str = sr.ReadToEnd();
                str = str.Replace(@"ref=""SQLAppender""", @"ref=""SQLAppenderError""");
                sr.Close();
                fs.Close();
                System.IO.FileStream fs1 = new System.IO.FileStream(logconfig, System.IO.FileMode.Open, System.IO.FileAccess.Write);
                StreamWriter swWriter = new StreamWriter(fs1, System.Text.Encoding.UTF8);
                swWriter.Flush();
                swWriter.Write(str);
                swWriter.Close();
                fs1.Close();
            }

            string exeFileFullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileNameWithoutExtension(exeFileFullPath);
            string binPath = Path.GetDirectoryName(exeFileFullPath);

            binPath = Path.GetDirectoryName(binPath);

            string logFilePath = Path.GetDirectoryName(binPath);

            var exe = Process.GetCurrentProcess();

            if (exe == null) return;

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string logPath = Path.Combine(documentPath,"HeBianGu", exeName, "Log");

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            InitLogPath(logPath);
        }

        #region 内部方法
        ILog Logger = null;
        void InitLogPath(string repository)
        {
            RollingFileAppender appender = new RollingFileAppender();
            appender.File = repository + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            appender.AppendToFile = true;
            appender.MaxSizeRollBackups = -1;
            //appender.MaximumFileSize = "1MB";  
            appender.RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Date;
            appender.DatePattern = "yyyy-MM-dd_HH\".log\"";
            appender.StaticLogFileName = false;
            appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
            appender.Layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level - %message%newline");
            appender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(appender);
        }

        /// <summary>
        /// 事件传送信息打印
        /// </summary>
        void EventsMsg(object sender, object e, System.Diagnostics.StackFrame SourceFile)
        {
            string msg = string.Format("[FILE:{0} ],LINE:{1},{2}] sender:{3},e:{4}", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod(), sender.GetType(), e.GetType());
            //Trace.WriteLine(msg);
            if (Logger != null)
            {
                Logger.Info(msg);
            }
        }

        string GetFileMsg(System.Diagnostics.StackFrame SourceFile)
        {
            return string.Format("FILE: [{0}] LINE:[{1}] Method:[{2}]", SourceFile.GetFileName(), SourceFile.GetFileLineNumber(), SourceFile.GetMethod());
        }

        void Error(System.Diagnostics.StackFrame SourceFile, Exception ex)
        {
            if (ex == null)
                return;
            if (Logger != null)
            {
                Logger.Info(GetFileMsg(SourceFile));
                Logger.Error(ex.Message);
                if (ex.InnerException != null)
                    Logger.Fatal(ex.InnerException);
            }
        }

        void Info(System.Diagnostics.StackFrame SourceFile, string infomsg)
        {
            if (infomsg == null) return;

            if (Logger == null) return;

            Logger.Info(GetFileMsg(SourceFile));
            Logger.Info(infomsg);
        }


        void ReplaceFileTag(string logconfig)
        {
            try
            {
                FileStream fs = new FileStream(logconfig, FileMode.Open, FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                fs.Close();

                if (str.IndexOf("#LOG_PATH#") > -1)
                {
                    str = str.Replace(@"#LOG_PATH#", Constants.SYS_TEMP_PATH);
                    FileStream fs1 = new FileStream(logconfig, FileMode.Open, FileAccess.Write);
                    StreamWriter swWriter = new StreamWriter(fs1, Encoding.UTF8);
                    swWriter.Flush();
                    swWriter.Write(str);
                    swWriter.Close();
                    fs1.Close();
                }
            }
            catch { }
        }
        #endregion
    }

    /// <summary>
    /// 系统需要使用到的常量 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 系统临时文件路径
        /// </summary>
        public static string SYS_TEMP_PATH = System.Environment.GetEnvironmentVariable("TEMP") + @"\";

    }
}
