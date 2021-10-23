using Service.Helper;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class FulfilmentOrder : IFulfilmentOrder
    {
        public OrderProduct GetOrders()
        {
            var orderProduct = JsonDeserializer.GetData();

            return orderProduct;
        }

        public IEnumerable<OrderProduct> GetOrdersByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                throw new Exception($"{nameof(GetOrdersByStatus)} parameter: {status} is null or empty");

            List<Product> product = GetOrders().Products;
            List<Order> order = GetOrders().Orders
                                .Where(o => o.Status == status)
                                .Select(i => i).ToList();

            IEnumerable<OrderProduct> statusOrder = new List<OrderProduct>() {
               new OrderProduct(product, order)
            };

            return statusOrder;
        }

        #region "Order Processing"

        public List<OrderItemAvailability> CalculateStockAvailability()
        {
            var pendingOrders = GetOrdersByStatus(nameof(OrderStatusEnum.Pending));
            List<OrderItemAvailability> orderItemsAvailability = new();

            if (pendingOrders != null)
                orderItemsAvailability = CheckCurrentStock(pendingOrders); //whether individual stocks can be fufilled

            return orderItemsAvailability;
        }

        /// <summary>
        /// Checks for the order items can be fulfiled and marks individual items as can/can't
        /// Decrements the stock availablity as we go, so it's always up to date.
        /// </summary>
        /// <param name="pendingOrders"></param>
        /// <returns></returns>
        private static List<OrderItemAvailability> CheckCurrentStock(IEnumerable<OrderProduct> pendingOrders)
        {
            List<Product> products = pendingOrders.FirstOrDefault().Products;
            List<Order> orders = pendingOrders.FirstOrDefault().Orders; //TO DO might have multiple orders too

            List<OrderItemAvailability> orderItemsAvailability = new();

            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    var stockAvailable = IsStockAvailable(products, item);

                    orderItemsAvailability.Add(new OrderItemAvailability(item.OrderId, item.ProductId, stockAvailable));
                    DecrementStock(item);
                }
            }
            return orderItemsAvailability;
        }

        private static bool IsStockAvailable(IEnumerable<Product> products, Item item)
        {
            var stockProduct = products.Single(p => p.ProductId == item.ProductId);

            return (stockProduct.QuantityOnHand >= item.Quantity);
        }

        private static void DecrementStock(Item item) // TO DO
        {
            //var stockProduct = fulfilmentOrder.GetOrdersByStatus("pending").Select(p => p.Products);
        }

        #endregion "Order Processing"
    }
}