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

namespace HeBianGu.General.DataBase.Identify
{
    public class DataContext : SqliteDataContextBase<DataContext>
    {
        public DataContext() : base(DataContext.GetDefaultConnection("identify.db"))
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SeedingDataInitializer<DataContext>(modelBuilder, l =>
             {
                 var role = new hi_dd_role { RoleName = "管理员", RoleCode = "001" };
                 this.hi_dd_roles.Add(role);

                 var user = new hi_dd_user { UserName = "系统管理员", Account = "admin", Password = "123456", hi_dd_role = role };
                 this.hi_dd_users.Add(user);
             });

            Database.SetInitializer(sqliteConnectionInitializer);
        }

        public virtual DbSet<hi_dd_user> hi_dd_users { get; set; }
        public virtual DbSet<hi_dd_role> hi_dd_roles { get; set; }
        public virtual DbSet<hi_dd_author> hi_dd_authors { get; set; }
        public virtual DbSet<hi_dd_log> hi_dd_logs { get; set; }
    }
}
