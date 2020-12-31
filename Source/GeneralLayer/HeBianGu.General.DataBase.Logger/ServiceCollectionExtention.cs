using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    public static class ServiceCollectionExtention
    {
        /// <summary> 注入单例模式 </summary>
        public static IServiceCollection UseLogger(this IServiceCollection service)
        {
            service.AddSingleton<DataContext>();

            service.AddSingleton<IInfoRespository, InfoRespository>();
            service.AddSingleton<IErrorRespository, ErrorRespository>();
            service.AddSingleton<IDebugRespository, DebugRespository>();
            service.AddSingleton<IFatalRespository, FatalRespository>();
            service.AddSingleton<IWarnRespository, WarnRespository>();

            service.AddSingleton<ILogService, LogService>();

            return service;
        }
    }
}
