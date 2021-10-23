using System.Collections.Generic;

namespace Service.Models
{
    public class OrderProduct
    {
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        public OrderProduct(List<Product> products, List<Order> orders)
        {
            this.Products = products;
            this.Orders = orders;
        }
    }
}
