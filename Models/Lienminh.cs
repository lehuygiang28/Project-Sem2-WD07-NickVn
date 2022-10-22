using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Lienminh
    {
        public Lienminh()
        {
            Images = new HashSet<Image>();
            Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public int ProductCategoryId { get; set; }
        public string ProductUserName { get; set; } = null!;
        public string ProductUserPassword { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public decimal PriceAtm { get; set; }
        public int Champ { get; set; }
        public int Skin { get; set; }
        public string Rank { get; set; } = null!;
        public string StatusAccount { get; set; } = null!;
        public string? Note { get; set; }
        public string ImgThumb { get; set; } = null!;
        public int StatusId { get; set; }

        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
