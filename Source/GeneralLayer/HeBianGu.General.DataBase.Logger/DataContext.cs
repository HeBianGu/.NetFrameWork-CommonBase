using HeBianGu.Common.DataBase.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    public class DataContext: SqliteDataContextBase<DataContext>
    {
        public DataContext():base(DataContext.GetDefaultConnection("logger.db"))
        {

        }
        public virtual DbSet<hl_dm_info> lp_dm_infos { get; set; }

        public virtual DbSet<hl_dm_error> lp_dm_errors { get; set; }

        public virtual DbSet<hl_dm_fatal> lp_dm_fatals { get; set; }

        public virtual DbSet<hl_dm_warn> lp_dm_warns { get; set; }

        public virtual DbSet<hl_dm_debug> lp_dm_debugs { get; set; } 
    }
}
