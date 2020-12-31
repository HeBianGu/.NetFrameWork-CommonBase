using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public class RoleRespository : RepositoryBase<hi_dd_role, Guid>, IRoleRespository
    {
        public RoleRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IRoleRespository : IRepository<hi_dd_role>
    {
    }
}
