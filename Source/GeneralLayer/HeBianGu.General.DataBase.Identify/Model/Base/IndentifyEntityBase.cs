using HeBianGu.Common.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public abstract class IndentifyEntityBase : GuidEntityBase
    {
        [Browsable(false)]
        [Display(Name = "创建时间")]
        [Column("create_time", Order = 20)]
        public string CDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Browsable(false)]
        [Display(Name = "修改时间")]
        [Column("edit_time", Order = 21)]
        public string UDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
         
        [Browsable(false)]
        [Display(Name = "是否可用")]
        [Column("is_enabled", Order = 22)]
        public bool IsEnabled { get; set; } = true;
    }
}
