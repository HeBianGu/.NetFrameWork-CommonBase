using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    
    public class FatalRespository : RepositoryBase<hl_dm_fatal, Guid>, IFatalRespository
    {
        public FatalRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IFatalRespository : IRepository<hl_dm_fatal>
    {
    }
}
