using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.SysConfig
{
    public class hc_dd_config : GuidEntityBase
    {
        [Column("create_time", Order = 20)]
        public string CDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Column("edit_time", Order = 21)]
        public string UDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Column("name", Order = 1)]
        public string Name { get; set; }

        [Required]
        [Column("type", Order = 2)]
        public string Type { get; set; }

        [Required]
        [Column("value", Order = 3)]
        public string Value { get; set; }

        [Column("verson", Order = 4)]
        public string Verson { get; set; }

        [Column("tag", Order = 5)]
        public string Tag { get; set; }


    }
}
