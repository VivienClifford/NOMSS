using API.Models;
using Service;
using Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Helper
{
    public class UnfulfillableOrder
    {
        public static Dictionary<string, List<int>> GetUnfulfillableOrders(OrderProduct orderProduct)
        {
            List<int> unfulfillable = orderProduct.Orders
                                      .Where(o => o.Status.Contains(nameof(OrderStatusEnum.Unfulfillable)))
                                      .Select(i => i.OrderId).ToList();

            var response = new ResponseStatusOrder(nameof(OrderStatusEnum.Unfulfillable), unfulfillable);
           
            return response.UnfulfillableOrders;
        }
    }
}
