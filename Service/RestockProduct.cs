using Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class RestockProduct : IRestockProduct
    {
        private readonly IFulfilmentOrder fulfilmentOrder;

        public RestockProduct()
        {
            this.fulfilmentOrder = new FulfilmentOrder();
        }

        /// <summary>
        /// Returns products which need to be restocked
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> RestockProducts()
        {
            return FilterProductsToRestock(fulfilmentOrder.CalculateStockAvailability());
        }

        /// <summary>
        /// Returns products which need to be restocked, using the data supplied
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> RestockProducts(OrderProduct orderProducts)
        {
            return FilterProductsToRestock(orderProducts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        private static IEnumerable<Product> FilterProductsToRestock(OrderProduct orderProduct)
        {
            var products = orderProduct.Products.Where(pr => pr.NeedRestock.Equals(true));

            return products;
        }
    }
}