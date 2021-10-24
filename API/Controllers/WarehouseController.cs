using API.Helper;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    public class WarehouseController : Controller
    {
        private readonly IFulfilmentOrder _fulfilmentOrder;
        private readonly IRestockProduct _restockProduct;

        public WarehouseController()
        {
            this._fulfilmentOrder = new FulfilmentOrder();
            this._restockProduct = new RestockProduct();
        }

        [HttpPost]
        [Route("api/v1/warehouse/fulfilment")]
        public Dictionary<string, List<int>> PostWarehouseFulfilment([FromBody] RequestWarehouseFulfilment request)
        {
            if (request.OrderIds == null)
                throw new Exception($"No {nameof(request.OrderIds)} passed in the body of the request"); //Test for other data types

            var orderProducts = _fulfilmentOrder.CalculateStockAvailability(request.OrderIds);

            return UnfulfillableOrder.GetUnfulfillableOrders(orderProducts);
        }

        [HttpGet]
        [Route("api/v1/warehouse/products/restock")]
        public IEnumerable<ResponseRestockProduct> GetWarehouseRestockProduct()
        {
            var products = _restockProduct.RestockProducts()
                           .Select(p => new ResponseRestockProduct(p.ProductId, p.Description, p.QuantityOnHand, p.ReorderAmount));

            return products;
        }
    }
}