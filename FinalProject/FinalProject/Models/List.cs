using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class List
    {
        public List()
        {
            MyLists = new HashSet<MyList>();
        }

        public int ListId { get; set; }
        public string ListName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<MyList> MyLists { get; set; }
    }
}
