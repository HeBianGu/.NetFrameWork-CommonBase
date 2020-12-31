using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    public class hl_dm_error : LogEntity
    {
        /// <summary> 参数信息 </summary>
        [Display(Name = "参数信息")] 
        [Column("parameter", Order = 3)]
        public string Parameter { get; set; }

        /// <summary> 异常消息 </summary>
        [Display(Name = "异常消息")]
        [Column("message", Order = 4)]
        public string Message { get; set; }

        /// <summary> 异常信息 </summary>
        [Display(Name = "异常详情")]
        [Column("excetion", Order = 5)]
        public string Exception { get; set; }

        /// <summary> 堆栈信息 </summary>
        [Display(Name = "堆栈信息")]
        [Column("stack", Order = 6)]
        public string Stack { get; set; }
    }
}
