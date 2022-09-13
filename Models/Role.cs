using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleNameEn { get; set; }
    }
}
