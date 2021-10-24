namespace API.Models
{
    public class ResponseRestockProduct
    {
        public int ProductId { get; set; }

        public string Description { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReorderAmount { get; set; }

        public ResponseRestockProduct(int ProductId, string Description, int QuantityOnHand, int ReorderAmount)
        {
            this.ProductId = ProductId;
            this.Description = Description;
            this.QuantityOnHand = QuantityOnHand;
            this.ReorderAmount = ReorderAmount;
        }
    }
}