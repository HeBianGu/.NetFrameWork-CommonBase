using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    
    public class WarnRespository : RepositoryBase<hl_dm_warn, Guid>, IWarnRespository
    {
        public WarnRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IWarnRespository : IRepository<hl_dm_warn>
    {
    }
}
