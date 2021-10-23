using Service.Helper;
using Service.Models;
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
            var orders = GetOrders()
                         .Orders.Where(o => o.Status == status)
                         .Select(o => o);

            return (IEnumerable<OrderProduct>)orders;
        }
    }
}
