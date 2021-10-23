using System.Collections.Generic;

namespace Service.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string DateCreated { get; set; }
        public List<Item> Items { get; set; }
    }
}
