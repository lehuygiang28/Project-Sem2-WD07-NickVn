using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Action { get; set; }
        public string ImgSrc { get; set; } = null!;
        public int Total { get; set; }
        public int? Note { get; set; }
        public int Status { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
