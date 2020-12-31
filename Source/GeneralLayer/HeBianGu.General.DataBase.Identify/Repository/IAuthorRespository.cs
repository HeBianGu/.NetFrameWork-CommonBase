using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    
    public class AuthorRespository : RepositoryBase<hi_dd_author, Guid>, IAuthorRespository
    {
        public AuthorRespository(DataContext dbContext) : base(dbContext)
        {

        }

    }

    public interface IAuthorRespository : IRepository<hi_dd_author>
    {
    }




}
