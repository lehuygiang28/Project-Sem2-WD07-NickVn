using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class TheNapDatum
    {
        public int Id { get; set; }
        public string TelecomName { get; set; } = null!;
        public int Amount { get; set; }
        public string Pin { get; set; } = null!;
        public string Serial { get; set; } = null!;
        public bool IsUse { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
