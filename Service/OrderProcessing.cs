using Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class OrderProcessing
    {
        /// <summary>
        /// Checks for the order items can be fulfiled
        /// </summary>
        /// <param name="pendingOrders"></param>
        /// <returns></returns>
        public static IEnumerable<OrderProduct> ProcessOrders(IEnumerable<OrderProduct> pendingOrders)
        {
            List<Product> products = pendingOrders.FirstOrDefault().Products;
            List<Order> orders = pendingOrders.FirstOrDefault().Orders; //TO DO might have multiple orders too

            foreach (var order in orders)
            {
                var updatedProducts = UpdateProductQuantity(products, order);

                if (updatedProducts != null)
                {
                    //order has been fulfilled
                    products = updatedProducts;
                    order.Status = nameof(OrderStatusEnum.Fulfilled); //need to test
                }
                else
                {
                    order.Status = nameof(OrderStatusEnum.Unfulfillable); //need to test, need to raise error
                }

            }

            return new List<OrderProduct>() { new OrderProduct(products, orders) };
        }

        /// <summary>
        ///  Decrements the stock availablity as we go, so the product list always up to date.
        ///  Calls ThresholdCalculator to check if we need to restock items / create a purchase order
        /// </summary>
        /// <param name="products"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        private static List<Product> UpdateProductQuantity(List<Product> products, Order order)
        {
            var updatedProducts = products;

            foreach (var item in order.Items)
            {
                var stockProduct = updatedProducts.Single(p => p.ProductId == item.ProductId);
                var itemQuantityAvailable = (stockProduct.QuantityOnHand >= item.Quantity);

                //we need to calculate whether the min threshold has been reached
                Restock.ThresholdCalculator(stockProduct, order);

                if (itemQuantityAvailable)
                {
                    stockProduct.QuantityOnHand -= item.Quantity;
                }
                else
                {
                    return null; //TO DO - can't fulfil order
                }

            }

            return updatedProducts;
        }
    }
}
