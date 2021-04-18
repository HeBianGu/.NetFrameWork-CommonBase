using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{

    public class hi_dd_role : IndentifyEntityBase
    {
        [Display(Name = "角色名称")] 
        [Column("role_name", Order = 1)]
        [Required]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        public string RoleName { get; set; }

        [Display(Name = "角色编码")] 
        [Column("role_code", Order = 2)]
        public string RoleCode { get; set; }

        public ICollection<hi_dd_author> Authors { get; set; }

        public ICollection<hi_dd_user> Users { get; set; }
    }
}
