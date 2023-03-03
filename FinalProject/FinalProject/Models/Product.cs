using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? ProductUrl { get; set; }
    }
}
