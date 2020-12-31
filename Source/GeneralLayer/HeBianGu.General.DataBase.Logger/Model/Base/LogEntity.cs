using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Logger
{
    public abstract class LogEntity : LogEntityBase
    {
        [Display(Name = "名称")]
        [Column("name", Order = 1)]
        public string Name { get; set; }

        [Display(Name = "级别")]
        [Column("level", Order = 2)]
        public int Level { get; set; } = 1;

    }

    public class LogEntity<T> : LogEntity
    {
        [Display(Name = "标题")]
        [Column("title", Order = 3)]
        public string Title { get; set; }

        [Display(Name = "值")]
        [Column("value", Order = 4)]
        public T Value { get; set; }

        [Display(Name = "消息")]
        [Column("message", Order = 5)]
        public string Message { get; set; }
    }
}
