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

        public IEnumerable<Product> RestockProducts(OrderProduct orderProducts)
        {
            return GetRestock(orderProducts);
        }

        private static IEnumerable<Product> GetRestock(OrderProduct orderProduct)
        {
            var products = orderProduct.Products.Where(pr => pr.NeedRestock.Equals(true));

            return products;
        }

    }
}
