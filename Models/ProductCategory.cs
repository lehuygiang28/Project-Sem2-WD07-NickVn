using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class ProductCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Action { get; set; }
        public string ImgSrc { get; set; } = null!;
        public int Total { get; set; }
        public int? Note { get; set; }
    }
}
