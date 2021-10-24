using Service.Models;

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
        OrderProduct GetOrdersByStatus(string status);

        /// <summary>
        /// Calculates stock availability and whether an order can be fulfilled.
        /// </summary>
        /// <returns></returns>
        OrderProduct CalculateStockAvailability();

        /// <summary>
        /// Calculates stock availability and whether an order can be fulfilled, using the supplied OrderIds
        /// </summary>
        /// <returns></returns>
        OrderProduct CalculateStockAvailability(int[] orderIds);

    }
}
