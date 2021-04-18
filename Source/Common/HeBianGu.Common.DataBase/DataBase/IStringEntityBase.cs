using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.DataBase
{


    /// <summary>
    /// 定义默认主键类型为Guid的实体基类
    /// </summary>
    public abstract class GuidEntityBase : EntityBase<Guid>
    {
        public GuidEntityBase()
        {
            this.ID = Guid.NewGuid();
        }
    }


    /// <summary>
    /// 定义默认主键类型为Guid的实体基类
    /// </summary>
    public abstract class StringEntityBase : EntityBase<string>
    {
        public StringEntityBase()
        {
            this.ID = Guid.NewGuid().ToString();

            //this.CDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //[Display(Name = "创建时间")]
        //[ReadOnly]
        //public string CDATE { get; set; }

        //[Display(Name = "修改时间")]
        //[ReadOnly]
        //public string UDATE { get; set; }

        //[Display(Name = "是否可用")]
        //[Hidden]
        //public int ISENBLED { get; set; } = 1;
    }
}
