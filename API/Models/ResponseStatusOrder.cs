using System.Collections.Generic;

namespace API.Models
{
    public class ResponseStatusOrder
    {
        public string ResponseStatus { get; set; }

        public List<int> OrderIds { get; set; }

        public ResponseStatusOrder(string responseStatus, List<int> OrderIds)
        {
            this.ResponseStatus = responseStatus;
            this.OrderIds = OrderIds;
        }

        public Dictionary<string, List<int>> UnfulfillableOrders
        {
            get
            {
                return new Dictionary<string, List<int>>()
                {
                    { ResponseStatus, OrderIds }
                };
            }
        }
    }
}
