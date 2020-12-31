using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    
    public class InfoRespository : RepositoryBase<hl_dm_info,Guid>, IInfoRespository
    {
        public InfoRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IInfoRespository : IRepository<hl_dm_info>
    {
    }
}
