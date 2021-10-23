using Service.Models;

namespace Service
{
    public class Restock
    {
        public static void ThresholdCalculator(Product product, Order order)
        {
            //TO DO: 
            RestockProducts(product, order);
        }

        public static void RestockProducts(Product product, Order order)
        {
            var productsToOrder = new RestockOrder(product);

            //TO DO stuff
        }
    }
}
