using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Status
    {
        public Status()
        {
            Lienminhs = new HashSet<Lienminh>();
            Users = new HashSet<User>();
        }

        public int StatusId { get; set; }
        public string StatusNameVi { get; set; } = null!;
        public string StatusNameEn { get; set; } = null!;

        public virtual ICollection<Lienminh> Lienminhs { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
