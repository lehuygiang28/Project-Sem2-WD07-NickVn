using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string ImgSaleOff { get; set; } = null!;
        public string ImgSrc { get; set; } = null!;
        public int Total { get; set; }
        public string Note { get; set; } = null!;
        public int Status { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
