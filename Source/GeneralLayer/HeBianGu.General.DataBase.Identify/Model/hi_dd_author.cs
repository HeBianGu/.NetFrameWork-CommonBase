using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public class hi_dd_author : IndentifyEntityBase
    {
        [Required]
        [Display(Name = "权限名称")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        [Column("author_name", Order = 1)]
        public string AuthorName { get; set; }

        [Display(Name = "权限编码")] 
        [Column("author_code", Order = 2)]
        public string AuthorCode { get; set; }

        public ICollection<hi_dd_role> Roles { get; set; }
    }
}
