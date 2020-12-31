using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.DataBase.Sqlite
{
    /// <summary> 设置种子数据 </summary>
    public class SeedingDataInitializer<T> : SqliteCreateDatabaseIfNotExists<T> where T : DbContext
    {
        public SeedingDataInitializer(DbModelBuilder builder) : base(builder)
        {

        }

        public SeedingDataInitializer(DbModelBuilder builder,Action<T> seedAction) : base(builder)
        {
            SeedAction = seedAction;
        }

        public Action<T> SeedAction { get; set; }

        protected override void Seed(T context)
        {
            //for (int i = 0; i < 6; i++)
            //{
            //    var item = new mbc_dc_case { Name = "Employer" + (i + 1) };
            //    context.mbc_dc_cases.Add(item);
            //}

            SeedAction?.Invoke(context);

            base.Seed(context);
        }
    }
}
