using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public static class ServiceCollectionExtention
    {
        /// <summary> 注入单例模式 </summary>
        public static IServiceCollection UseIndentify(this IServiceCollection service)
        {
            service.AddSingleton<DataContext>();

            service.AddSingleton<IAuthorRespository, AuthorRespository>();
            service.AddSingleton<IUserRespository, UserRespository>();
            service.AddSingleton<IRoleRespository, RoleRespository>();
            service.AddSingleton<ILogRespository, LogRespository>();

            service.AddSingleton<IIdentifyService, IdentifyService>();

            return service;
        }
    }
}
