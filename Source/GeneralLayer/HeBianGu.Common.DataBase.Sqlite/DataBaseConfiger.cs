using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.DataBase.Sqlite
{
    /// <summary> 数据库连接配置 </summary>
    internal class DataBaseConfiger
    {
        /// <summary> 连接对象 </summary>
        public static DbConnection GetCon
        {
            get
            {
                // Todo ：取文档下面的数据库 
                string v = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HeBianGu",
                    Assembly.GetEntryAssembly().GetName().Name, "database.db");
                v = "Data Source=" + v.Replace(@"\\", @"\") + ";";

                SQLiteConnection conn = new SQLiteConnection(v);

                return conn;
            }

        }

    }
}
