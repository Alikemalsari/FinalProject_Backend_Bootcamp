using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string? AdSoyad { get; set; }
        public string EPosta { get; set; } = null!;
        public string Sifre { get; set; } = null!;
    }
}
