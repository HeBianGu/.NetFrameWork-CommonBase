using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.SysConfig
{
    public static class ServiceCollectionExtention
    {
        /// <summary> 注入单例模式 </summary>
        public static IServiceCollection UseConfig(this IServiceCollection service)
        {
            service.AddSingleton<DataContext>();

            service.AddSingleton<IConfigRespository, ConfigRespository>();

            service.AddSingleton<IConfigService, ConfigService>();

            return service;
        }
    }
}
