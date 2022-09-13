using System;
using System.Collections.Generic;

namespace Project_Sem2_WD07_NickVn.Models
{
    public partial class Googlerecaptcha
    {
        public int Id { get; set; }
        public string? HostName { get; set; }
        public string SiteKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string? Response { get; set; }
    }
}
