using Service.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IFulfilmentOrder
    {
        /// <summary>
        /// Returns all orders
        /// </summary>
        /// <returns></returns>
        OrderProduct GetOrders();

        /// <summary>
        /// Returns all orders matching the status
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderProduct> GetOrdersByStatus(string status);

        /// <summary>
        /// Calculates stock availability and whether an order can be fulfilled.
        /// </summary>
        /// <returns></returns>
        List<OrderItemAvailability> CalculateStockAvailability();

    }
}
