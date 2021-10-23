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
            List<OrderProduct> processedOrders = new();

            foreach (var pendingOrder in pendingOrders) {

                processedOrders = UpdateStatus(pendingOrder);
            }

            return processedOrders;
        }

        /// <summary>
        /// Updates the order status
        /// </summary>
        /// <param name="pendingOrder"></param>
        /// <returns></returns>
        private static List<OrderProduct> UpdateStatus(OrderProduct pendingOrder)
        {
            List<Product> products = pendingOrder.Products;
            List<Order> orders = pendingOrder.Orders;

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
                    order.Status = $"Error: {nameof(OrderStatusEnum.Unfulfillable)}"; //need to test, need to raise error
                }

            }
            return new List<OrderProduct>() { new OrderProduct(products, orders) };
        }

        /// <summary>
        ///  Decrements the stock quantity as we go, so the product list always up to date.
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

                if (itemQuantityAvailable)
                {
                    stockProduct.QuantityOnHand -= item.Quantity;
                }
                else
                {
                    return null;
                }

            }

            return updatedProducts;
        }
    }
}
