﻿using HeBianGu.Common.DataBase;
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
    public class hi_dd_user : IndentifyEntityBase
    {
        [Display(Name = "用户名称")]
        [Required]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        [Column("user_name", Order = 1)]
        public string UserName { get; set; }

        [Display(Name = "登陆账号")]
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{4,15}$", ErrorMessage = "字母开头，允许5-16字节，允许字母数字下划线！")]
        [Column("account", Order = 2)]
        public string Account { get; set; }

        [Display(Name = "登陆密码")]
        [Required]
        [Column("password", Order = 3)]
        [RegularExpression(@"^[0-9]{5,17}$", ErrorMessage = "以字母开头，长度在6~18之间，只能包含字母、数字和下划线！！")]
        public string Password { get; set; }

        [Display(Name = "昵称")]
        [Column("display", Order = 4)]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "角色")]
        [Editor("Combobox", "Combobox")]
        [Column("role_id", Order = 5)]
        public Guid RoleID { get; set; }

        [Column("role", Order = 6)] 
        [Browsable(false)]
        public hi_dd_role Role { get; set; }
    }
   
}
