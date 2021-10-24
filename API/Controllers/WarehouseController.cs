using API.Helper;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;


namespace API.Controllers
{
    [ApiController]
    public class WarehouseController : Controller
    {

        private readonly IFulfilmentOrder fulfilmentOrder;

        public WarehouseController() {
            this.fulfilmentOrder = new FulfilmentOrder();
        }
        
        [HttpPost]
        [Route("api/v1/warehouse/fulfilment")]
        public Dictionary<string, List<int>> PostWarehouseFulfilment([FromBody] RequestWarehouseFulfilment request)
        {
            if (request.OrderIds == null)
                throw new Exception($"No {nameof(request.OrderIds)} passed in the body of the request"); //Test for other data types

            var orderProducts = fulfilmentOrder.CalculateStockAvailability(request.OrderIds);

            return UnfulfillableOrder.GetUnfulfillableOrders(orderProducts);
        }
    }
}
