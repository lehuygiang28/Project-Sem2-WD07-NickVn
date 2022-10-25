using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class ChargeHistory
    {
        public int PhoneCardHistoryId { get; set; }
        public int UserId { get; set; }
        public string Telecom { get; set; } = null!;
        public string Pin { get; set; } = null!;
        public string Serial { get; set; } = null!;
        public string TypeCharge { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal MoneyReceived { get; set; }
        public string? Note { get; set; }
        public string Result { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
