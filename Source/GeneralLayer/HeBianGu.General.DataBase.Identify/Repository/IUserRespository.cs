using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public class UserRespository : RepositoryBase<hi_dd_user, Guid>, IUserRespository
    {
        public UserRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IUserRespository : IRepository<hi_dd_user>
    {
    }
}
