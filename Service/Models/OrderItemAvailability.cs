namespace Service.Models
{
    public class OrderItemAvailability
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public bool StockAvailable { get; set; }

        public OrderItemAvailability(int orderId, int productId, bool stockAvailable)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.StockAvailable = stockAvailable;
        }
    }

}
