using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.SysConfig
{
    public class hc_dd_config : GuidEntityBase
    {
        [Browsable(false)]
        [Display(Name ="创建时间")]
        //[ReadOnly]
        [Column("create_time", Order = 20)]
        public DateTime CDate { get; set; } = DateTime.Now;

        [Browsable(false)]
        [Display(Name = "修改时间")] 
        [Column("edit_time", Order = 21)]
        public string UDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Display(Name = "名称")]
        [Required]
        [Column("name", Order = 1)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "类型")] 
        [Column("type", Order = 2)]
        public string Type { get; set; }

        [Required]
        [Display(Name = "值")] 
        [Column("value", Order = 3)]
        public string Value { get; set; }

        [Display(Name = "版本")] 
        [Column("verson", Order = 4)]
        public string Verson { get; set; }

        [Display(Name = "说明")] 
        [Column("tag", Order = 5)]
        public string Tag { get; set; }
         
        [Display(Name = "是否可用")]
        [Column("is_enabled", Order = 19)]
        public bool IsEnabled { get; set; } = true;


    }
}
