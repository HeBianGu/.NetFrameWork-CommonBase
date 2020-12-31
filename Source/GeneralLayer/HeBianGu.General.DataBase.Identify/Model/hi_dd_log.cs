﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.DataBase.Identify
{
    public class hi_dd_log : IndentifyEntityBase
    {
        [Display(Name = "操作账号")] 
        [Column("account", Order = 1)]
        public string Account { get; set; }

        [Display(Name = "内容")] 
        [Column("message", Order = 2)]
        public string Message { get; set; }

        [Display(Name = "操作")] 
        [Column("title", Order = 3)]
        public string Title { get; set; }
    }
}
