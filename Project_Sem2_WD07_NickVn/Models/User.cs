using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class User
    {
        public User()
        {
            ChargeHistories = new HashSet<ChargeHistory>();
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public decimal Money { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string? ImgSrc { get; set; }
        public string CoverImgSrc { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<ChargeHistory> ChargeHistories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
