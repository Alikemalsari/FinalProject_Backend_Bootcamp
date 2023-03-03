using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class MyList
    {
        public int CurrentListId { get; set; }
        public int ListId { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }

        public virtual List List { get; set; } = null!;
    }
}
