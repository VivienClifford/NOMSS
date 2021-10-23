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

        public IEnumerable<OrderProduct> CalculateStockAvailability()
        {
            var pendingOrders = GetOrdersByStatus(nameof(OrderStatusEnum.Pending));

            return OrderProcessing.ProcessOrders(pendingOrders);

        }
    }
}