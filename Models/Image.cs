using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Image
    {
        public int ImgId { get; set; }
        public int ProductId { get; set; }
        public string ImgLink { get; set; } = null!;

        public virtual Lienminh Product { get; set; } = null!;
    }
}
