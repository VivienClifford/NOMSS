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

        /// <summary>
        /// Returns the OrderProduct with a filtered order list by chosen status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public OrderProduct GetOrdersByStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
                throw new Exception($"{nameof(GetOrdersByStatus)} parameter: {status} is null or empty");

            List<Product> product = GetOrders().Products;
            List<Order> order = GetOrders().Orders
                                .Where(o => o.Status == status)
                                .Select(i => i).ToList();

            OrderProduct statusOrder = new(product, order);
            
            return statusOrder;
        }

        /// <summary>
        /// Processes the order and calculates stock and by using default data
        /// </summary>
        /// <returns></returns>
        public OrderProduct CalculateStockAvailability()
        {
            var pendingOrders = GetOrdersByStatus(nameof(OrderStatusEnum.Pending));

            return OrderProcess.ProcessOrders(pendingOrders);
        }

        /// <summary>
        /// Processes the order and calculates stock and by using default filtered data by selected orderIds.
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public OrderProduct CalculateStockAvailability(int[] orderIds)
        {
            var product = GetOrdersByStatus(nameof(OrderStatusEnum.Pending)).Products;
            var order = GetOrdersByStatus(nameof(OrderStatusEnum.Pending)).Orders;

            List<Order> orderWithMatchingIds = order.FindAll(x => orderIds.Contains(x.OrderId));

            OrderProduct filteredOrders = new(product, orderWithMatchingIds);

            return OrderProcess.ProcessOrders(filteredOrders);

        }
    }
}