using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public class LogRespository : RepositoryBase<hi_dd_log, Guid>, ILogRespository
    {
        public LogRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface ILogRespository : IRepository<hi_dd_log>
    {
    }
}
