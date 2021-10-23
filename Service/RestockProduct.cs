using Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class RestockProduct : IRestockProduct
    {

        private readonly IFulfilmentOrder fulfilmentOrder = new FulfilmentOrder();


        public IEnumerable<Product> RestockProducts()
        {
            return GetRestock(fulfilmentOrder.CalculateStockAvailability());
        }

        public IEnumerable<Product> RestockProducts(IEnumerable<OrderProduct> orderProducts)
        {
            return GetRestock(orderProducts);
        }

        private static IEnumerable<Product> GetRestock(IEnumerable<OrderProduct> orderProducts)
        {
            var products = orderProducts.Select(p => p.Products.Where(pr => pr.NeedRestock.Equals(true)));

            return (IEnumerable<Product>)products;
        }

    }
}
