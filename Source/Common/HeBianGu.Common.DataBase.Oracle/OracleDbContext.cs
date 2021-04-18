using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.DataBase.Oracle
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext() : base("name=OracleDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<OracleDbContext>());
        }

        public OracleDbContext(DbConnection dbConnection) : base(dbConnection, false)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<OracleDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  Message：限制所有string类型生成nvarchart
            modelBuilder.Properties().Where(p => p.PropertyType == typeof(String) && p.GetCustomAttributes(typeof(StringLengthAttribute), false).Length == 0).Configure(p => p.HasMaxLength(2000));

            var sp = this.Database.Connection.ConnectionString.Split(';');

            var u = sp.Where(x => !string.IsNullOrEmpty(x) && x.Contains("=")).FirstOrDefault(y => y.Trim().ToLower().StartsWith("user"));

            if (u == null)
                throw new ArgumentException($"ConnectionString {this.Database.Connection.ConnectionString} is invalid !");

            sp = u.Split('=');

           string useid = sp[1].Trim().ToUpper();

            modelBuilder.HasDefaultSchema(useid);

            base.OnModelCreating(modelBuilder);

        }
    }

}
