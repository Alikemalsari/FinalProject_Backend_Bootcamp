using FinalProject.Models;

namespace FinalProject.ViewModel
{
    public class MyListViewModel
    {
        public int ListId { get; set; }
        public string ListName { get; set; }
        public bool isActive { get; set; }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? Quantity { get; set; }
        
    }
}
