using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.SysConfig
{
    public class ConfigRespository : RepositoryBase<hc_dd_config, Guid>, IConfigRespository
    {
        public ConfigRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IConfigRespository : IRepository<hc_dd_config>
    {
    }
}
