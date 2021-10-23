namespace Service.Models
{
    public class RestockOrder
    {
        public Product Product { get; set; } //products to restock

        public RestockOrder(Product product)
        {
            this.Product = product;
        }
    }
}
