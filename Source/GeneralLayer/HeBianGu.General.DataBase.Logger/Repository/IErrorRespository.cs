using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    
    public class ErrorRespository : RepositoryBase<hl_dm_error,Guid>, IErrorRespository
    {
        public ErrorRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IErrorRespository : IRepository<hl_dm_error>
    {
    }
}
