using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Lienminh
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public decimal PriceAtm { get; set; }
        public int Champ { get; set; }
        public int Skin { get; set; }
        public string Rank { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int? Note { get; set; }
        public string ImgThumb { get; set; } = null!;
        public string ImgSrc { get; set; } = null!;
    }
}
