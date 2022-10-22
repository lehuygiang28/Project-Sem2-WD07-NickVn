using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Oder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
