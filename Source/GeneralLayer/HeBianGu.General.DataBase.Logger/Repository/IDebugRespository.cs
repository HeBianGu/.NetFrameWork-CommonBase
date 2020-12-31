using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    
    public class DebugRespository : RepositoryBase<hl_dm_debug, Guid>, IDebugRespository
    {
        public DebugRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IDebugRespository : IRepository<hl_dm_debug>
    {
    }
}
