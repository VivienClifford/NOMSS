using System.Collections.Generic;

namespace Service.Models
{
    public class OrderProduct
    {
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
}
