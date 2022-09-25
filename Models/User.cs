using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public decimal Money { get; set; }
        public int RoleId { get; set; }
        public string? ImgSrc { get; set; }
        public string CoverImgSrc { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
