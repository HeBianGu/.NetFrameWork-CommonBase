using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.DataBase.Sqlite
{
    public class SqliteDataContextBase<T> : DbContext where T : DbContext
    {

        public SqliteDataContextBase() : base(DataBaseConfiger.GetCon, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SeedingDataInitializer<T>(modelBuilder);

            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }


}
