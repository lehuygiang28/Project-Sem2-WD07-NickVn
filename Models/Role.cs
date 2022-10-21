using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string RoleNameVi { get; set; } = null!;
        public string? RoleNameEn { get; set; }
    }
}
