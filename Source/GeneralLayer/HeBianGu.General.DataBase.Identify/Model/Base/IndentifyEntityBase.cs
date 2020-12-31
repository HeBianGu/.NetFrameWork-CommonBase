using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public abstract class IndentifyEntityBase : GuidEntityBase
    {
        [Column("create_time", Order = 20)]
        public string CDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Column("edit_time", Order = 21)]
        public string UDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Column("is_enabled", Order = 22)]
        public bool IsEnabled { get; set; } = true;
    }
}
