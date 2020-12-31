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

namespace HeBianGu.General.DataBase.SysConfig
{
    public class DataContext : SqliteDataContextBase<DataContext>
    {
        public DataContext() : base(DataContext.GetDefaultConnection("config.db"))
        {
            
        } 

        public virtual DbSet<hc_dd_config> hc_dd_configs { get; set; }
    }
}
