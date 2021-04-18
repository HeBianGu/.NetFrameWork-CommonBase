using SQLite.CodeFirst;
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

        public SqliteDataContextBase(DbConnection con) : base(con, true)
        {

        }
        public SqliteDataContextBase() : base(DataBaseConfiger.GetCon, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SeedingDataInitializer<T>(modelBuilder);

            //var sqliteConnectionInitializer = new CreateDatabaseIfNotExists<T>(modelBuilder);

            //var sqliteConnectionInitializer = new SqliteDropCreateDatabaseAlways<T>(modelBuilder);


            Database.SetInitializer(sqliteConnectionInitializer);
        }


        /// <summary> 连接对象 </summary>
        public static DbConnection GetDefaultConnection(string dbName)
        {
            // Todo ：取文档下面的数据库 
            string v = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HeBianGu",
                Assembly.GetEntryAssembly().GetName().Name, dbName);
            v = "Data Source=" + v.Replace(@"\\", @"\") + ";";

            return new SQLiteConnection(v);

        }
    }


}
